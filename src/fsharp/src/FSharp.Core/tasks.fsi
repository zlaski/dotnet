// TaskBuilder.fs - TPL task computation expressions for F#
//
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.FSharp.Control

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Collections

/// <summary>
/// The extra data stored in ResumableStateMachine for tasks
/// </summary>
[<Struct; NoComparison; NoEquality>]
[<CompilerMessage("This construct  is for use by compiled F# code and should not be used directly",
                  1204,
                  IsHidden = true)>]
type TaskStateMachineData<'T> =

    /// <summary>
    /// Holds the final result of the state machine
    /// </summary>
    [<DefaultValue(false)>]
    val mutable Result: 'T

    /// <summary>
    /// Holds the MethodBuilder for the state machine
    /// </summary>
    [<DefaultValue(false)>]
    val mutable MethodBuilder: AsyncTaskMethodBuilder<'T>

/// <summary>
/// This is used by the compiler as a template for creating state machine structs
/// </summary>
and [<CompilerMessage("This construct  is for use by compiled F# code and should not be used directly",
                      1204,
                      IsHidden = true)>] TaskStateMachine<'TOverall> =
    ResumableStateMachine<TaskStateMachineData<'TOverall>>

/// <summary>
/// Represents the runtime continuation of a task state machine created dynamically
/// </summary>
and [<CompilerMessage("This construct  is for use by compiled F# code and should not be used directly",
                      1204,
                      IsHidden = true)>] TaskResumptionFunc<'TOverall> = ResumptionFunc<TaskStateMachineData<'TOverall>>

/// <summary>
/// A special compiler-recognised delegate type for specifying blocks of task code
/// with access to the state machine.
/// </summary>
and [<CompilerMessage("This construct  is for use by compiled F# code and should not be used directly",
                      1204,
                      IsHidden = true)>] TaskCode<'TOverall, 'T> = ResumableCode<TaskStateMachineData<'TOverall>, 'T>

/// <summary>
/// Contains methods to build tasks using the F# computation expression syntax
/// </summary>
[<Class>]
type TaskBuilderBase =

    /// <summary>
    /// Specifies the sequential composition of two units of task code.
    /// </summary>
    member inline Combine: task1: TaskCode<'TOverall, unit> * task2: TaskCode<'TOverall, 'T> -> TaskCode<'TOverall, 'T>

    /// <summary>
    /// Specifies the delayed execution of a unit of task code.
    /// </summary>
    member inline Delay: generator: (unit -> TaskCode<'TOverall, 'T>) -> TaskCode<'TOverall, 'T>

    /// <summary>
    /// Specifies the iterative execution of a unit of task code.
    /// </summary>
    member inline For: sequence: seq<'T> * body: ('T -> TaskCode<'TOverall, unit>) -> TaskCode<'TOverall, unit>

    /// <summary>
    /// Specifies a unit of task code which returns a value
    /// </summary>
    member inline Return: value: 'T -> TaskCode<'T, 'T>

    /// <summary>
    /// Specifies a unit of task code which executed using try/finally semantics
    /// </summary>
    member inline TryFinally:
        body: TaskCode<'TOverall, 'T> * [<InlineIfLambda>] compensation: (unit -> unit) -> TaskCode<'TOverall, 'T>

    /// <summary>
    /// Specifies a unit of task code which executed using try/with semantics
    /// </summary>
    member inline TryWith:
        body: TaskCode<'TOverall, 'T> * catch: (exn -> TaskCode<'TOverall, 'T>) -> TaskCode<'TOverall, 'T>

#if NETSTANDARD2_1
    /// <summary>
    /// Specifies a unit of task code which binds to the resource implementing IAsyncDisposable and disposes it asynchronously
    /// </summary>
    member inline Using<'Resource, 'TOverall, 'T when 'Resource :> IAsyncDisposable> :
        resource: 'Resource * body: ('Resource -> TaskCode<'TOverall, 'T>) -> TaskCode<'TOverall, 'T>
#endif

    /// <summary>
    /// Specifies the iterative execution of a unit of task code.
    /// </summary>
    member inline While: condition: (unit -> bool) * body: TaskCode<'TOverall, unit> -> TaskCode<'TOverall, unit>

    /// <summary>
    /// Specifies a unit of task code which produces no result
    /// </summary>
    [<DefaultValue>]
    member inline Zero: unit -> TaskCode<'TOverall, unit>

/// <summary>
/// Contains methods to build tasks using the F# computation expression syntax
/// </summary>
[<Class>]
type TaskBuilder =
    inherit TaskBuilderBase

    /// <summary>
    /// The entry point for the dynamic implementation of the corresponding operation. Do not use directly, only used when executing quotations that involve tasks or other reflective execution of F# code.
    /// </summary>
    static member RunDynamic: code: TaskCode<'T, 'T> -> Task<'T>

    /// Hosts the task code in a state machine and starts the task.
    member inline Run: code: TaskCode<'T, 'T> -> Task<'T>

/// <summary>
/// Contains methods to build tasks using the F# computation expression syntax
/// </summary>
[<Class>]
type BackgroundTaskBuilder =
    inherit TaskBuilderBase

    /// <summary>
    /// The entry point for the dynamic implementation of the corresponding operation. Do not use directly, only used when executing quotations that involve tasks or other reflective execution of F# code.
    /// </summary>
    static member RunDynamic: code: TaskCode<'T, 'T> -> Task<'T>

    /// <summary>
    /// Hosts the task code in a state machine and starts the task, executing in the threadpool using Task.Run
    /// </summary>
    member inline Run: code: TaskCode<'T, 'T> -> Task<'T>

/// Contains the `task` computation expression builder.
[<AutoOpen>]
module TaskBuilder =

    /// <summary>
    /// Builds a task using computation expression syntax.
    /// </summary>
    ///
    /// <example-tbd></example-tbd>
    val task: TaskBuilder

    /// <summary>
    /// Builds a task using computation expression syntax which switches to execute on a background thread if not
    /// already doing so.
    /// </summary>
    ///
    /// <remarks>
    /// If the task is created on a foreground thread (where <see cref="P:System.Threading.SynchronizationContext.Current"/> is non-null)
    /// its body is executed on a background thread using <see cref="M:System.Threading.Tasks.Task.Run"/>.
    /// If created on a background thread (where <see cref="P:System.Threading.SynchronizationContext.Current"/> is null) it is executed
    /// immediately on that thread.
    /// </remarks>
    ///
    /// <example-tbd></example-tbd>
    val backgroundTask: BackgroundTaskBuilder

// Contains the `task` computation expression builder.
namespace Microsoft.FSharp.Control.TaskBuilderExtensions

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Control
open Microsoft.FSharp.Core.CompilerServices

/// <summary>
/// Contains low-priority overloads for the `task` computation expression builder.
/// </summary>
//
// Note: they are low priority because they are auto-opened first, and F# has a rule
// that extension method opened later in sequence get higher priority
//
// AutoOpen is by assembly attribute to get sequencing of AutoOpen correct and
// so each gives different priority
module LowPriority =

    type TaskBuilderBase with

        /// <summary>
        /// Specifies a unit of task code which draws a result from a task-like value
        /// satisfying the GetAwaiter pattern and calls a continuation.
        /// </summary>
        [<NoEagerConstraintApplication>]
        member inline Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TOverall> :
            task: ^TaskLike * continuation: ('TResult1 -> TaskCode<'TOverall, 'TResult2>) ->
                TaskCode<'TOverall, 'TResult2>
                when ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
                and ^Awaiter :> ICriticalNotifyCompletion
                and ^Awaiter: (member get_IsCompleted: unit -> bool)
                and ^Awaiter: (member GetResult: unit -> 'TResult1)

        /// <summary>
        /// Specifies a unit of task code which draws its result from a task-like value
        /// satisfying the GetAwaiter pattern.
        /// </summary>
        [<NoEagerConstraintApplication>]
        member inline ReturnFrom< ^TaskLike, ^Awaiter, 'T> :
            task: ^TaskLike -> TaskCode<'T, 'T>
                when ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
                and ^Awaiter :> ICriticalNotifyCompletion
                and ^Awaiter: (member get_IsCompleted: unit -> bool)
                and ^Awaiter: (member GetResult: unit -> 'T)

        /// <summary>
        /// The entry point for the dynamic implementation of the corresponding operation. Do not use directly, only used when executing quotations that involve tasks or other reflective execution of F# code.
        /// </summary>
        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TOverall> :
            sm: byref<TaskStateMachine<'TOverall>> *
            task: ^TaskLike *
            continuation: ('TResult1 -> TaskCode<'TOverall, 'TResult2>) ->
                bool
                when ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
                and ^Awaiter :> ICriticalNotifyCompletion
                and ^Awaiter: (member get_IsCompleted: unit -> bool)
                and ^Awaiter: (member GetResult: unit -> 'TResult1)

        /// <summary>
        /// Specifies a unit of task code which binds to the resource implementing IDisposable and disposes it synchronously
        /// </summary>
        member inline Using:
            resource: 'Resource * body: ('Resource -> TaskCode<'TOverall, 'T>) -> TaskCode<'TOverall, 'T>
                when 'Resource :> IDisposable|null

/// <summary>
/// Contains medium-priority overloads for the `task` computation expression builder.
/// </summary>
module MediumPriority =

    type TaskBuilderBase with

        /// <summary>
        /// Specifies a unit of task code which draws a result from an F# async value then calls a continuation.
        /// </summary>
        member inline Bind:
            computation: Async<'TResult1> * continuation: ('TResult1 -> TaskCode<'TOverall, 'TResult2>) ->
                TaskCode<'TOverall, 'TResult2>

        /// <summary>
        /// Specifies a unit of task code which draws a result from an F# async value.
        /// </summary>
        member inline ReturnFrom: computation: Async<'T> -> TaskCode<'T, 'T>

/// <summary>
/// Contains high-priority overloads for the `task` computation expression builder.
/// </summary>
module HighPriority =

    type TaskBuilderBase with

        /// <summary>
        /// Specifies a unit of task code which draws a result from a task then calls a continuation.
        /// </summary>
        member inline Bind:
            task: Task<'TResult1> * continuation: ('TResult1 -> TaskCode<'TOverall, 'TResult2>) ->
                TaskCode<'TOverall, 'TResult2>

        /// <summary>
        /// Specifies a unit of task code which draws a result from a task.
        /// </summary>
        member inline ReturnFrom: task: Task<'T> -> TaskCode<'T, 'T>

        /// <summary>
        /// The entry point for the dynamic implementation of the corresponding operation. Do not use directly, only used when executing quotations that involve tasks or other reflective execution of F# code.
        /// </summary>
        static member BindDynamic:
            sm: byref<TaskStateMachine<'TOverall>> *
            task: Task<'TResult1> *
            continuation: ('TResult1 -> TaskCode<'TOverall, 'TResult2>) ->
                bool
