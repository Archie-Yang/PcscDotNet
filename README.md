# PcscDotNet

.NET standard library for accessing *PC/SC* (*Personal Computer/Smart Card*) functions.

> This library provides the generic way for accessing *PC/SC* functions, and you can write your specific provider with [`IPcscProvider`][] interface implementation.

---

## Projects

- [src/PcscDotNet](src/PcscDotNet)
    > *PcscDotNet* library.
- [tests/PcscDotNet.ConsoleTests](tests/PcscDotNet.ConsoleTests)
    > Console tests.

---

## Introduction

The implementations of *PC/SC* enumerations and structures, the declarations of *PC/SC* methods, both are referenced from *Windows Kit*, *MSDN* and *pcsc-lite*.

The main interfaces/classes:

- [`IPcscProvider`][] Interface
- [`Pcsc`][] Class
- [`Pcsc<TIPcscProvider>`][] Class
- [`WinSCard`][] Class

[`IPcscProvider`]: #ipcscprovider-interface
[`Pcsc`]: #pcsc-class
[`Pcsc<TIPcscProvider>`]: #pcsctipcscprovider-class
[`WinSCard`]: #winscard-class

### [IPcscProvider Interface](src/PcscDotNet/IPcscProvider.cs "Go to Source")

This interface declares the members that need to be implemented for accessing *PC/SC* functions.

These are the methods declared with the same name of *PC/SC* functions currently:

- `SCardCancel`
- `SCardEstablishContext`
- `SCardFreeMemory`
- `SCardGetStatusChange`
- `SCardIsValidContext`
- `SCardListReaders`
- `SCardReleaseContext`

Other methods:

- `AllocateReaderStatus`
  > Allocates the instance of `PcscReaderStatus` (`PcscReaderStatus<TISCardReaderState>`) with the specific blittable type of structure which has implemented `ISCardReaderState` interface.
- `AllocateString`
  > Allocates string in managed and unmanaged memory.
- `FreeString`
  > Releases the unmanaged string allocated by `AllocateString`.

If you want to implement your provider using `WinSCard` or `pcsc-lite`, see the table below, it shows the different definitions between platforms:

| Unmanaged Defined              | *    | `WinSCard` (*Windows*) | `pcsc-lite` (*OS X*) | `pcsc-lite` (*Linux*) |
| ------------------------------ | ---- | ---------------------- | -------------------- | --------------------- |
| `DWORD`                        | Size | 4                      | 4                    | Size of Pointer       |
| `LONG`                         | Size | 4                      | 4                    | Size of Pointer       |
| `SCARD_READERSTATE`            | Pack | Default                | 1                    | Default               |
| `rgbAtr` (`SCARD_READERSTATE`) | Size | 36                     | 33                   | 33                    |

### [Pcsc Class](src/PcscDotNet/Pcsc.cs "Go to Source")

This class is the start point for accessing *PC/SC* functions. You need to specify [`IPcscProvider`][] instance to create [`Pcsc`][] instance.

### [Pcsc\<TIPcscProvider\> Class](src/PcscDotNet/Pcsc_1.cs "Go to Source")

This class provides the static members with corresponding members in [`Pcsc`][] class, using singleton instance of `TIPcscProvider` which implements [`IPcscProvider`][] interface.

> Most of time, `TIPcscProvider` can be used with singleton safely unless its members may be changed, e.g., the provider loads functions from different library dynamically.

#### [WinSCard Class](src/PcscDotNet/WinSCard.cs "Go to Source")

This class implements [`IPcscProvider`][] using `WinSCard.dll` of *Windows* environment.

---

Archie Yang