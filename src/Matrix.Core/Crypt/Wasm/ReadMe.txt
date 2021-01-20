WASM on NET5 has no support for a coupld of crypto algorithmns.
When code is calling them it throws an PlatformNotSupportedException.

They should be added back to WASM with NET6. For now we need to 
include them in managed code and handle the PlatformNotSupportedException.

This is what all the files in the folder are for. Once NET6 is out we should be able
to remove those. Until then we need them for WASM/Blazor support

The code of the managed algortithms was taken from Microsoft .NET Framework codes and 
different versions of it.

see also:
* https://github.com/dotnet/runtime/issues/40074
* https://github.com/dotnet/runtime/issues/44996