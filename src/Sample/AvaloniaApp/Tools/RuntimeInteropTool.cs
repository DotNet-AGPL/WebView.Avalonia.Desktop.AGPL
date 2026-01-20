

namespace AvaloniaApp.Tools;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RuntimeInteropTool
{
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.MarshalAsAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.InAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.OutAttribute))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.UnmanagedType))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(System.Runtime.InteropServices.CharSet))]
    public static void RegisterRuntimeInteropDependencyType() 
    {
        Console.WriteLine("register the dependency type of runtime-interop");
        /*
        System.Runtime.InteropServices.Architecture;
        System.Runtime.InteropServices.ArrayWithOffset;
        System.Runtime.InteropServices.AutomationProxyAttribute;
        System.Runtime.InteropServices.BestFitMappingAttribute;
        System.Runtime.InteropServices.CallingConvention;
        System.Runtime.InteropServices.CharSet;
        System.Runtime.InteropServices.ClassInterfaceAttribute;
        System.Runtime.InteropServices.ClassInterfaceType;
        System.Runtime.InteropServices.CLong;
        System.Runtime.InteropServices.CoClassAttribute;
        System.Runtime.InteropServices.CollectionsMarshal;
        System.Runtime.InteropServices.ComAliasNameAttribute;
        System.Runtime.InteropServices.ComCompatibleVersionAttribute;
        System.Runtime.InteropServices.ComConversionLossAttribute;
        System.Runtime.InteropServices.ComDefaultInterfaceAttribute;
        System.Runtime.InteropServices.COMException;
        System.Runtime.InteropServices.ComImportAttribute;
        System.Runtime.InteropServices.ComInterfaceType;
        System.Runtime.InteropServices.ComMemberType;
        System.Runtime.InteropServices.ComRegisterFunctionAttribute;
        System.Runtime.InteropServices.ComTypes.IDataObject;
        System.Runtime.InteropServices.ComTypes.IEnumSTATDATA;
        System.Runtime.InteropServices.ComUnregisterFunctionAttribute;
        System.Runtime.InteropServices.ComVisibleAttribute;
        System.Runtime.InteropServices.ComWrappers;
        System.Runtime.InteropServices.CreateComInterfaceFlags;
        System.Runtime.InteropServices.CreateObjectFlags;
        System.Runtime.InteropServices.CriticalHandle;
        System.Runtime.InteropServices.CULong;
        System.Runtime.InteropServices.DefaultCharSetAttribute;
        System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute;
        System.Runtime.InteropServices.DefaultParameterValueAttribute;
        System.Runtime.InteropServices.DispIdAttribute;
        System.Runtime.InteropServices.DllImportAttribute;
        System.Runtime.InteropServices.DllImportResolver;
        System.Runtime.InteropServices.DllImportSearchPath;
        System.Runtime.InteropServices.DynamicInterfaceCastableImplementationAttribute;
        System.Runtime.InteropServices.ExternalException;
        System.Runtime.InteropServices.FieldOffsetAttribute;
        System.Runtime.InteropServices.GCHandle;
        System.Runtime.InteropServices.GCHandleType;
        System.Runtime.InteropServices.GuidAttribute;
        System.Runtime.InteropServices.HandleCollector;
        System.Runtime.InteropServices.HandleRef;
        System.Runtime.InteropServices.ICustomFactory;
        System.Runtime.InteropServices.ICustomMarshaler;
        System.Runtime.InteropServices.IDynamicInterfaceCastable;
        System.Runtime.InteropServices.ImmutableCollectionsMarshal;
        System.Runtime.InteropServices.ImportedFromTypeLibAttribute;
        System.Runtime.InteropServices.InAttribute;
        System.Runtime.InteropServices.InterfaceTypeAttribute;
        System.Runtime.InteropServices.InvalidComObjectException;
        System.Runtime.InteropServices.InvalidOleVariantTypeException;
        System.Runtime.InteropServices.JavaScript.JSException;
        System.Runtime.InteropServices.JavaScript.JSExportAttribute;
        System.Runtime.InteropServices.JavaScript.JSHost;
        System.Runtime.InteropServices.JavaScript.JSImportAttribute;
        System.Runtime.InteropServices.JavaScript.JSMarshalAsAttribute;
        System.Runtime.InteropServices.JavaScript.JSObject;
        System.Runtime.InteropServices.JavaScript.JSType;
        System.Runtime.InteropServices.JsonMarshal;
        System.Runtime.InteropServices.LayoutKind;
        System.Runtime.InteropServices.LCIDConversionAttribute;
        System.Runtime.InteropServices.LibraryImportAttribute;
        System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute;
        System.Runtime.InteropServices.Marshal;
        System.Runtime.InteropServices.MarshalAsAttribute;
        System.Runtime.InteropServices.MarshalDirectiveException;
        System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller;
        System.Runtime.InteropServices.Marshalling.ArrayMarshaller;
        System.Runtime.InteropServices.Marshalling.BStrStringMarshaller;
        System.Runtime.InteropServices.Marshalling.ComExposedClassAttribute;
        System.Runtime.InteropServices.Marshalling.ComInterfaceMarshaller;
        System.Runtime.InteropServices.Marshalling.ComInterfaceOptions;
        System.Runtime.InteropServices.Marshalling.ComObject;
        System.Runtime.InteropServices.Marshalling.ComVariant;
        System.Runtime.InteropServices.Marshalling.ComVariantMarshaller;
        System.Runtime.InteropServices.Marshalling.ContiguousCollectionMarshallerAttribute;
        System.Runtime.InteropServices.Marshalling.CustomMarshallerAttribute;
        System.Runtime.InteropServices.Marshalling;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        System.Runtime.InteropServices;
        */
    }
}
