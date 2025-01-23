// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//

#include "stdafx.h"

const BYTE CMiniMdBase::s_ModuleCol[] = {2,
  iUSHORT,0,2,  iSTRING,2,2,  iGUID,4,2,  iGUID,6,2,  iGUID,8,2,
  iUSHORT,0,2,  iSTRING,2,4,  iGUID,6,2,  iGUID,8,2,  iGUID,10,2,
};
const BYTE CMiniMdBase::s_TypeRefCol[] = {2,
  75,0,2,  iSTRING,2,2,  iSTRING,4,2,
  75,0,2,  iSTRING,2,4,  iSTRING,6,4,
};
const BYTE CMiniMdBase::s_TypeDefCol[] = {2,
  iULONG,0,4,  iSTRING,4,2,  iSTRING,6,2,  64,8,2,  4,10,2,  6,12,2,
  iULONG,0,4,  iSTRING,4,4,  iSTRING,8,4,  64,12,2,  4,14,2,  6,16,2,
};
const BYTE CMiniMdBase::s_FieldPtrCol[] = {1,
  4,0,2,
};
const BYTE CMiniMdBase::s_FieldCol[] = {3,
  iUSHORT,0,2,  iSTRING,2,2,  iBLOB,4,2,
  iUSHORT,0,2,  iSTRING,2,4,  iBLOB,6,4,
  iUSHORT,0,2,  iSTRING,2,4,  iBLOB,6,2,
};
const BYTE CMiniMdBase::s_MethodPtrCol[] = {1,
  6,0,2,
};
const BYTE CMiniMdBase::s_MethodCol[] = {3,
  iULONG,0,4,  iUSHORT,4,2,  iUSHORT,6,2,  iSTRING,8,2,  iBLOB,10,2,  8,12,2,
  iULONG,0,4,  iUSHORT,4,2,  iUSHORT,6,2,  iSTRING,8,4,  iBLOB,12,4,  8,16,2,
  iULONG,0,4,  iUSHORT,4,2,  iUSHORT,6,2,  iSTRING,8,4,  iBLOB,12,2,  8,14,2,
};
const BYTE CMiniMdBase::s_ParamPtrCol[] = {1,
  8,0,2,
};
const BYTE CMiniMdBase::s_ParamCol[] = {2,
  iUSHORT,0,2,  iUSHORT,2,2,  iSTRING,4,2,
  iUSHORT,0,2,  iUSHORT,2,2,  iSTRING,4,4,
};
const BYTE CMiniMdBase::s_InterfaceImplCol[] = {1,
  2,0,2,  64,2,2,
};
const BYTE CMiniMdBase::s_MemberRefCol[] = {3,
  69,0,2,  iSTRING,2,2,  iBLOB,4,2,
  69,0,4,  iSTRING,4,4,  iBLOB,8,4,
  69,0,2,  iSTRING,2,4,  iBLOB,6,2,
};
const BYTE CMiniMdBase::s_ConstantCol[] = {3,
  100,0,1,  65,2,2,  iBLOB,4,2,
  100,0,1,  65,2,4,  iBLOB,6,4,
  100,0,1,  65,2,2,  iBLOB,4,4,
};
const BYTE CMiniMdBase::s_CustomAttributeCol[] = {3,
  66,0,2,  74,2,2,  iBLOB,4,2,
  66,0,4,  74,4,4,  iBLOB,8,4,
  66,0,4,  74,4,2,  iBLOB,6,2,
};
const BYTE CMiniMdBase::s_FieldMarshalCol[] = {2,
  67,0,2,  iBLOB,2,2,
  67,0,2,  iBLOB,2,4,
};
const BYTE CMiniMdBase::s_DeclSecurityCol[] = {3,
  96,0,2,  68,2,2,  iBLOB,4,2,
  96,0,2,  68,2,4,  iBLOB,6,4,
  96,0,2,  68,2,2,  iBLOB,4,4,
};
const BYTE CMiniMdBase::s_ClassLayoutCol[] = {1,
  iUSHORT,0,2,  iULONG,2,4,  2,6,2,
};
const BYTE CMiniMdBase::s_FieldLayoutCol[] = {1,
  iULONG,0,4,  4,4,2,
};
const BYTE CMiniMdBase::s_StandAloneSigCol[] = {2,
  iBLOB,0,2,
  iBLOB,0,4,
};
const BYTE CMiniMdBase::s_EventMapCol[] = {1,
  2,0,2,  20,2,2,
};
const BYTE CMiniMdBase::s_EventPtrCol[] = {1,
  20,0,2,
};
const BYTE CMiniMdBase::s_EventCol[] = {2,
  iUSHORT,0,2,  iSTRING,2,2,  64,4,2,
  iUSHORT,0,2,  iSTRING,2,4,  64,6,2,
};
const BYTE CMiniMdBase::s_PropertyMapCol[] = {1,
  2,0,2,  23,2,2,
};
const BYTE CMiniMdBase::s_PropertyPtrCol[] = {1,
  23,0,2,
};
const BYTE* CMiniMdBase::s_PropertyCol = s_FieldCol;
const BYTE CMiniMdBase::s_MethodSemanticsCol[] = {1,
  iUSHORT,0,2,  6,2,2,  70,4,2,
};
const BYTE CMiniMdBase::s_MethodImplCol[] = {1,
  2,0,2,  71,2,2,  71,4,2,
};
const BYTE CMiniMdBase::s_ModuleRefCol[] = {2,
  iSTRING,0,2,
  iSTRING,0,4,
};
const BYTE* CMiniMdBase::s_TypeSpecCol = s_StandAloneSigCol;
const BYTE CMiniMdBase::s_ImplMapCol[] = {2,
  iUSHORT,0,2,  72,2,2,  iSTRING,4,2,  26,6,2,
  iUSHORT,0,2,  72,2,2,  iSTRING,4,4,  26,8,2,
};
const BYTE* CMiniMdBase::s_FieldRVACol = s_FieldLayoutCol;
const BYTE CMiniMdBase::s_ENCLogCol[] = {1,
  iULONG,0,4,  iULONG,4,4,
};
const BYTE CMiniMdBase::s_ENCMapCol[] = {1,
  iULONG,0,4,
};
const BYTE CMiniMdBase::s_AssemblyCol[] = {3,
  iULONG,0,4,  iUSHORT,4,2,  iUSHORT,6,2,  iUSHORT,8,2,  iUSHORT,10,2,  iULONG,12,4,  iBLOB,16,2,  iSTRING,18,2,  iSTRING,20,2,
  iULONG,0,4,  iUSHORT,4,2,  iUSHORT,6,2,  iUSHORT,8,2,  iUSHORT,10,2,  iULONG,12,4,  iBLOB,16,4,  iSTRING,20,4,  iSTRING,24,4,
  iULONG,0,4,  iUSHORT,4,2,  iUSHORT,6,2,  iUSHORT,8,2,  iUSHORT,10,2,  iULONG,12,4,  iBLOB,16,2,  iSTRING,18,4,  iSTRING,22,4,
};
const BYTE* CMiniMdBase::s_AssemblyProcessorCol = s_ENCMapCol;
const BYTE CMiniMdBase::s_AssemblyOSCol[] = {1,
  iULONG,0,4,  iULONG,4,4,  iULONG,8,4,
};
const BYTE CMiniMdBase::s_AssemblyRefCol[] = {3,
  iUSHORT,0,2,  iUSHORT,2,2,  iUSHORT,4,2,  iUSHORT,6,2,  iULONG,8,4,  iBLOB,12,2,  iSTRING,14,2,  iSTRING,16,2,  iBLOB,18,2,
  iUSHORT,0,2,  iUSHORT,2,2,  iUSHORT,4,2,  iUSHORT,6,2,  iULONG,8,4,  iBLOB,12,4,  iSTRING,16,4,  iSTRING,20,4,  iBLOB,24,4,
  iUSHORT,0,2,  iUSHORT,2,2,  iUSHORT,4,2,  iUSHORT,6,2,  iULONG,8,4,  iBLOB,12,2,  iSTRING,14,4,  iSTRING,18,4,  iBLOB,22,2,
};
const BYTE CMiniMdBase::s_AssemblyRefProcessorCol[] = {1,
  iULONG,0,4,  35,4,2,
};
const BYTE CMiniMdBase::s_AssemblyRefOSCol[] = {1,
  iULONG,0,4,  iULONG,4,4,  iULONG,8,4,  35,12,2,
};
const BYTE CMiniMdBase::s_FileCol[] = {3,
  iULONG,0,4,  iSTRING,4,2,  iBLOB,6,2,
  iULONG,0,4,  iSTRING,4,4,  iBLOB,8,4,
  iULONG,0,4,  iSTRING,4,4,  iBLOB,8,2,
};
const BYTE CMiniMdBase::s_ExportedTypeCol[] = {2,
  iULONG,0,4,  iULONG,4,4,  iSTRING,8,2,  iSTRING,10,2,  73,12,2,
  iULONG,0,4,  iULONG,4,4,  iSTRING,8,4,  iSTRING,12,4,  73,16,2,
};
const BYTE CMiniMdBase::s_ManifestResourceCol[] = {2,
  iULONG,0,4,  iULONG,4,4,  iSTRING,8,2,  73,10,2,
  iULONG,0,4,  iULONG,4,4,  iSTRING,8,4,  73,12,2,
};
const BYTE CMiniMdBase::s_NestedClassCol[] = {1,
  2,0,2,  2,2,2,
};
const BYTE CMiniMdBase::s_GenericParamCol[] = {2,
  iUSHORT,0,2,  iUSHORT,2,2,  76,4,2,  iSTRING,6,2,  64,8,2,  64,10,2,
  iUSHORT,0,2,  iUSHORT,2,2,  76,4,2,  iSTRING,6,4,  64,10,2,  64,12,2,
};
const BYTE CMiniMdBase::s_MethodSpecCol[] = {2,
  71,0,2,  iBLOB,2,2,
  71,0,2,  iBLOB,2,4,
};
const BYTE CMiniMdBase::s_GenericParamConstraintCol[] = {1,
  42,0,2,  64,2,2,
};
#ifdef FEATURE_METADATA_EMIT_PORTABLE_PDB
// Dummy descriptors to fill the gap to 0x30
// Our parsing process assumes that each colum def will have at least 1 entry,
// so add enough bytes for a full table descriptor (1 + sizeof(CMiniColDef) bytes)
const BYTE CMiniMdBase::s_Dummy1Col[] = { 0, 0, 0, 0 };
const BYTE CMiniMdBase::s_Dummy2Col[] = { 0, 0, 0, 0 };
const BYTE CMiniMdBase::s_Dummy3Col[] = { 0, 0, 0, 0 };
// Actual portable PDB tables descriptors
const BYTE CMiniMdBase::s_DocumentCol[] = { 2,
  iBLOB,0,2, iGUID,2,2, iBLOB,4,2, iGUID,6,2,
  iBLOB,0,4, iGUID,4,2, iBLOB,6,2, iGUID,8,4,
};
const BYTE CMiniMdBase::s_MethodDebugInformationCol[] = { 2,
  48,0,2,  iBLOB,2,2,
  48,0,2,  iBLOB,2,4,
};
const BYTE CMiniMdBase::s_LocalScopeCol[] = { 1,
  6,0,2,   53,2,2,  51,4,2,  52,6,2,  iULONG,8,4,  iULONG,12,4
};
const BYTE CMiniMdBase::s_LocalVariableCol[] = { 2,
  iUSHORT,0,2,  iUSHORT,2,2,  iSTRING,4,2,
  iUSHORT,0,2,  iUSHORT,2,2,  iSTRING,4,4
};
const BYTE CMiniMdBase::s_LocalConstantCol[] = { 3,
  iSTRING,0,2, iBLOB,2,2,
  iSTRING,0,4, iBLOB,4,4,
  iSTRING,0,4, iBLOB,4,2,
};
const BYTE CMiniMdBase::s_ImportScopeCol[] = { 2,
  53,0,2,  iBLOB,2,2,
  53,0,2,  iBLOB,2,4
};
// TODO:
// const BYTE CMiniMdBase::s_StateMachineMethodCol[] = {};
// const BYTE CMiniMdBase::s_CustomDebugInformationCol[] = {};
#endif // #ifdef FEATURE_METADATA_EMIT_PORTABLE_PDB

const BYTE* const CMiniMdBase::s_TableColumnDescriptors[] = {
s_ModuleCol,
s_TypeRefCol,
s_TypeDefCol,
s_FieldPtrCol,
s_FieldCol,
s_MethodPtrCol,
s_MethodCol,
s_ParamPtrCol,
s_ParamCol,
s_InterfaceImplCol,
s_MemberRefCol,
s_ConstantCol,
s_CustomAttributeCol,
s_FieldMarshalCol,
s_DeclSecurityCol,
s_ClassLayoutCol,
s_FieldLayoutCol,
s_StandAloneSigCol,
s_EventMapCol,
s_EventPtrCol,
s_EventCol,
s_PropertyMapCol,
s_PropertyPtrCol,
s_FieldCol,
s_MethodSemanticsCol,
s_MethodImplCol,
s_ModuleRefCol,
s_StandAloneSigCol,
s_ImplMapCol,
s_FieldLayoutCol,
s_ENCLogCol,
s_ENCMapCol,
s_AssemblyCol,
s_ENCMapCol,
s_AssemblyOSCol,
s_AssemblyRefCol,
s_AssemblyRefProcessorCol,
s_AssemblyRefOSCol,
s_FileCol,
s_ExportedTypeCol,
s_ManifestResourceCol,
s_NestedClassCol,
s_GenericParamCol,
s_MethodSpecCol,
s_GenericParamConstraintCol,
#ifdef FEATURE_METADATA_EMIT_PORTABLE_PDB
// Dummy descriptors to fill the gap to 0x30
s_Dummy1Col,
s_Dummy2Col,
s_Dummy3Col,
// Actual portable PDB tables descriptors
s_DocumentCol,
s_MethodDebugInformationCol,
s_LocalScopeCol,
s_LocalVariableCol,
s_LocalConstantCol,
s_ImportScopeCol,
// TODO:
// s_StateMachineMethodCol,
// s_CustomDebugInformationCol,
#endif // #ifdef FEATURE_METADATA_EMIT_PORTABLE_PDB
};
