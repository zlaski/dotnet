// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//
//
// This file was generated, please do not edit it directly.
//
// Please see MilCodeGen.html for more information.
//

#if PRESENTATION_CORE
#else
using SR=System.Windows.SR;
#endif

namespace System.Windows
{
    /// <summary>
    ///     TextDecorationUnit - The unit type of text decoration value
    /// </summary>
    public enum TextDecorationUnit
    {
        /// <summary>
        ///     FontRecommended - The unit is the calculated value by layout system
        /// </summary>
        FontRecommended = 0,

        /// <summary>
        ///     FontRenderingEmSize - The unit is the rendering Em size
        /// </summary>
        FontRenderingEmSize = 1,

        /// <summary>
        ///     Pixel - The unit is one pixel
        /// </summary>
        Pixel = 2,
    }   
}
