using System.Runtime.InteropServices;

namespace SampSharp.ColAndreas.Entities.Interop;

/// <summary>
/// Raw P/Invoke surface to ColAndreas's <c>CASharp_*</c> exports
/// (declared in <c>src/csharp_api.h</c> of the plugin, implemented in
/// <c>csharp_api.cpp</c>). Each wrapper is a thin pointer-args / value-rets
/// forwarder over <c>collisionWorld-&gt;...</c> in <c>DynamicWorld.cpp</c>.
///
/// All ray-cast methods follow the same convention as the legacy AMX
/// natives: returns the hit model id (or col-id, depending on the variant)
/// when the ray hits something, 0 otherwise. Output pointers are left
/// untouched on miss.
/// </summary>
internal static partial class ColAndreasInterop
{
    private const string Library = "colandreas";

    // ---- diagnostics ---------------------------------------------------

    [LibraryImport(Library, EntryPoint = "CASharp_IsDataLoaded")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int IsDataLoaded();

    [LibraryImport(Library, EntryPoint = "CASharp_IsInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int IsInitialized();

    [LibraryImport(Library, EntryPoint = "CASharp_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void Init();

    // ---- ray casts -----------------------------------------------------

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastLine")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastLine(
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz);

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastLineExtraID")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastLineExtraID(
        int type,
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz);

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastLineID")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastLineID(
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz);

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastLineEx")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastLineEx(
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz,
        float* qx, float* qy, float* qz, float* qw,
        float* px, float* py, float* pz);

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastLineAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastLineAngle(
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz,
        float* rx, float* ry, float* rz);

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastReflectionVector")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastReflectionVector(
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz,
        float* refx, float* refy, float* refz);

    [LibraryImport(Library, EntryPoint = "CASharp_RayCastLineNormal")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int RayCastLineNormal(
        float sx, float sy, float sz,
        float ex, float ey, float ez,
        float* hx, float* hy, float* hz,
        float* nx, float* ny, float* nz);

    // ---- objects -------------------------------------------------------

    [LibraryImport(Library, EntryPoint = "CASharp_CreateObject")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int CreateObject(
        int modelId,
        float x, float y, float z,
        float rx, float ry, float rz,
        int addToManager);

    [LibraryImport(Library, EntryPoint = "CASharp_DestroyObject")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int DestroyObject(int colIndex);

    [LibraryImport(Library, EntryPoint = "CASharp_IsValidObject")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int IsValidObject(int colIndex);

    [LibraryImport(Library, EntryPoint = "CASharp_SetObjectPos")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int SetObjectPos(int colIndex, float x, float y, float z);

    [LibraryImport(Library, EntryPoint = "CASharp_SetObjectRot")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int SetObjectRot(int colIndex, float rx, float ry, float rz);

    [LibraryImport(Library, EntryPoint = "CASharp_SetObjectExtraID")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int SetObjectExtraID(int colIndex, int type, int data);

    [LibraryImport(Library, EntryPoint = "CASharp_GetObjectExtraID")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int GetObjectExtraID(int colIndex, int type);

    // ---- buildings -----------------------------------------------------

    [LibraryImport(Library, EntryPoint = "CASharp_RemoveBuilding")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int RemoveBuilding(int model, float x, float y, float z, float radius);

    [LibraryImport(Library, EntryPoint = "CASharp_RestoreBuilding")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int RestoreBuilding(int model, float x, float y, float z, float radius);

    // ---- queries -------------------------------------------------------

    [LibraryImport(Library, EntryPoint = "CASharp_ContactTest")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int ContactTest(
        int modelId,
        float x, float y, float z,
        float rx, float ry, float rz);

    [LibraryImport(Library, EntryPoint = "CASharp_GetModelBoundingSphere")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int GetModelBoundingSphere(
        int modelId,
        float* centerX, float* centerY, float* centerZ,
        float* radius);

    [LibraryImport(Library, EntryPoint = "CASharp_GetModelBoundingBox")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial int GetModelBoundingBox(
        int modelId,
        float* minX, float* minY, float* minZ,
        float* maxX, float* maxY, float* maxZ);

    // ---- math helpers --------------------------------------------------

    [LibraryImport(Library, EntryPoint = "CASharp_EulerToQuat")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial void EulerToQuat(
        float rx, float ry, float rz,
        float* qx, float* qy, float* qz, float* qw);

    [LibraryImport(Library, EntryPoint = "CASharp_QuatToEuler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static unsafe partial void QuatToEuler(
        float qx, float qy, float qz, float qw,
        float* rx, float* ry, float* rz);
}
