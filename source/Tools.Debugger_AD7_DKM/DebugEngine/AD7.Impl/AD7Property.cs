using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Debugger.Interop;

namespace nanoFramework.Tools.VisualStudio.DebugEngine
{
    // An implementation of IDebugProperty2
    // This interface represents a stack frame property, a program document property, or some other property. 
    // The property is usually the result of an expression evaluation. 
    //
    // The sample engine only supports locals and parameters for functions that have symbols loaded.
    class AD7Property : IDebugProperty2
    {
        
        private VariableInformation m_variableInformation;

        public AD7Property(VariableInformation vi)
        {
            m_variableInformation = vi;
        }

        // Construct a DEBUG_PROPERTY_INFO representing this local or parameter.
        public DEBUG_PROPERTY_INFO ConstructDebugPropertyInfo(enum_DEBUGPROP_INFO_FLAGS dwFields)
        {
            DEBUG_PROPERTY_INFO propertyInfo = new DEBUG_PROPERTY_INFO();

            if ((dwFields & enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_FULLNAME) != 0)
            {
                propertyInfo.bstrFullName = m_variableInformation.m_name;
                propertyInfo.dwFields |= enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_FULLNAME;
            }

            if ((dwFields & enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_NAME) != 0)
            {
                propertyInfo.bstrName = m_variableInformation.m_name;
                propertyInfo.dwFields |= enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_NAME;
            }

            if ((dwFields & enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_TYPE) != 0)
            {
                propertyInfo.bstrType = m_variableInformation.m_typeName;
                propertyInfo.dwFields |= enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_TYPE;
            }

            if ((dwFields & enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_VALUE) != 0)
            {
                propertyInfo.bstrValue = m_variableInformation.m_value;
                propertyInfo.dwFields |= enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_VALUE;
            }

            if ((dwFields & enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_ATTRIB) != 0)
            {
                // The sample does not support writing of values displayed in the debugger, so mark them all as read-only.
                propertyInfo.dwAttrib |= enum_DBG_ATTRIB_FLAGS.DBG_ATTRIB_VALUE_READONLY;

                if (this.m_variableInformation.child != null)
                {
                    propertyInfo.dwAttrib |= enum_DBG_ATTRIB_FLAGS.DBG_ATTRIB_OBJ_IS_EXPANDABLE;
                }
            }

            // If the debugger has asked for the property, or the property has children (meaning it is a pointer in the sample)
            // then set the pProperty field so the debugger can call back when the chilren are enumerated.
            if (((dwFields & enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_PROP) != 0) ||
                (this.m_variableInformation.child != null))
            {
                propertyInfo.pProperty = (IDebugProperty2)this;
                propertyInfo.dwFields |= enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_PROP;
            }

            return propertyInfo;
        }

        #region IDebugProperty2 Members

        // Enumerates the children of a property. This provides support for dereferencing pointers, displaying members of an array, or fields of a class or struct.
        // The sample debugger only supports pointer dereferencing as children. This means there is only ever one child.
        public int EnumChildren(enum_DEBUGPROP_INFO_FLAGS dwFields, uint dwRadix, ref System.Guid guidFilter, enum_DBG_ATTRIB_FLAGS dwAttribFilter, string pszNameFilter, uint dwTimeout, out IEnumDebugPropertyInfo2 ppEnum)
        {
            ppEnum = null;

            if (this.m_variableInformation.child != null)
            {
                DEBUG_PROPERTY_INFO[] properties = new DEBUG_PROPERTY_INFO[1];
                properties[0] = (new AD7Property(this.m_variableInformation.child)).ConstructDebugPropertyInfo(dwFields);
                ppEnum = new AD7PropertyEnum(properties);
                return Constants.S_OK;
            }

            return Constants.S_FALSE;
        }

        // Returns the property that describes the most-derived property of a property
        // This is called to support object oriented languages. It allows the debug engine to return an IDebugProperty2 for the most-derived 
        // object in a hierarchy. This engine does not support this.
        public int GetDerivedMostProperty(out IDebugProperty2 ppDerivedMost)
        {
            throw new NotImplementedException();
        }

        // This method exists for the purpose of retrieving information that does not lend itself to being retrieved by calling the IDebugProperty2::GetPropertyInfo 
        // method. This includes information about custom viewers, managed type slots and other information.
        // The sample engine does not support this.
        public int GetExtendedInfo(ref System.Guid guidExtendedInfo, out object pExtendedInfo)
        {
            throw new NotImplementedException();
        }

        // Returns the memory bytes for a property value.
        public int GetMemoryBytes(out IDebugMemoryBytes2 ppMemoryBytes)
        {
            throw new NotImplementedException();
        }

        // Returns the memory context for a property value.
        public int GetMemoryContext(out IDebugMemoryContext2 ppMemory)
        {
            throw new NotImplementedException();
        }

        // Returns the parent of a property.
        // The sample engine does not support obtaining the parent of properties.
        public int GetParent(out IDebugProperty2 ppParent)
        {
            throw new NotImplementedException();
        }

        // Fills in a DEBUG_PROPERTY_INFO structure that describes a property.
        public int GetPropertyInfo(enum_DEBUGPROP_INFO_FLAGS dwFields, uint dwRadix, uint dwTimeout, IDebugReference2[] rgpArgs, uint dwArgCount, DEBUG_PROPERTY_INFO[] pPropertyInfo)
        {
            pPropertyInfo[0] = new DEBUG_PROPERTY_INFO();
            rgpArgs = null;
            pPropertyInfo[0] = ConstructDebugPropertyInfo(dwFields);
            return Constants.S_OK;
        }

        //  Return an IDebugReference2 for this property. An IDebugReference2 can be thought of as a type and an address.
        public int GetReference(out IDebugReference2 ppReference)
        {
            throw new NotImplementedException();
        }

        // Returns the size, in bytes, of the property value.
        public int GetSize(out uint pdwSize)
        {
            throw new NotImplementedException();
        }

        // The debugger will call this when the user tries to edit the property's values
        // the sample has set the read-only flag on its properties, so this should not be called.
        public int SetValueAsReference(IDebugReference2[] rgpArgs, uint dwArgCount, IDebugReference2 pValue, uint dwTimeout)
        {
            throw new NotImplementedException();
        }

        // The debugger will call this when the user tries to edit the property's values in one of the debugger windows.
        // the sample has set the read-only flag on its properties, so this should not be called.
        public int SetValueAsString(string pszValue, uint dwRadix, uint dwTimeout)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
