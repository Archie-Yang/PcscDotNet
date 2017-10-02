# PcscDotNet

.NET standard library for accessing *PC/SC* (*Personal Computer/Smart Card*) functions.

> This library provides the generic way for accessing *PC/SC* functions, you can write your specific provider with [`IPcscProvider`][] interface implementation.

---

## Projects

- [src/PcscDotNet](src/PcscDotNet)
  > *PcscDotNet* library.
- [tests/PcscDotNet.ConsoleTests](tests/PcscDotNet.ConsoleTests)
  > Console tests.

---

## Introduction

The implementations of *PC/SC* enumerations and structures and the declarations of *PC/SC* methods, both are referenced from *Windows Kit*, *MSDN* and *pcsc-lite*.

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
- `SCardListReaderGroups`
- `SCardListReaders`
- `SCardReleaseContext`

Other methods:

- `AllocateReaderStates`
  > Allocates managed byte array which mapped to unmanaged array of `SCARD_READERSTATE` structure.
- `AllocateString`
  > Allocates string in managed and unmanaged memory.
- `FreeString`
  > Releases the unmanaged string allocated by `AllocateString`.
- `ReadReaderState`
  > Reads values from the specific index of the `SCARD_READERSTATE` array which allocated by `AllocateReaderStates` method.
- `WriteReaderState`
  > Writes values to the specific index of the `SCARD_READERSTATE` array which allocated by `AllocateReaderStates` method.

If you want to implement your provider using `WinSCard` or `pcsc-lite`, see the table below, it shows the different definitions between platforms:

| Unmanaged Defined          | *        | `WinSCard` (*Windows*) | `pcsc-lite` (*OS X*) | `pcsc-lite` (*Linux*) |
| -------------------------- | -------- | ---------------------- | -------------------- | --------------------- |
| *ANSI* Characters          | Encoding | System Locale          | *UTF-8*              | *UTF-8*               |
| *Unicode* Characters       | Encoding | *Unicode*              | -                    | -                     |
| `SCARD_READERSTATE`        | Pack     | -                      | `1`                  | -                     |
| `DWORD`                    | Size     | `4`                    | `4`                  | `sizeof(void*)`       |
| `LONG`                     | Size     | `4`                    | `4`                  | `sizeof(void*)`       |
| `SCARD_READERSTATE.rgbAtr` | Size     | `36`                   | `33`                 | `33`                  |
| `SCARD_PROTOCOL_RAW`       | Value    | `0x00010000`           | `0x04`               | `0x04`                |

### [Pcsc Class](src/PcscDotNet/Pcsc.cs "Go to Source")

This class is the start point for accessing *PC/SC* functions. You need to specify [`IPcscProvider`][] instance to create [`Pcsc`][] instance.

### [Pcsc\<TIPcscProvider\> Class](src/PcscDotNet/Pcsc_1.cs "Go to Source")

This class provides the static members with corresponding members in [`Pcsc`][] class, using singleton instance of `TIPcscProvider` which implements [`IPcscProvider`][] interface.

> Most of time, `TIPcscProvider` can be used with singleton safely unless its members may be changed, e.g., the provider loads functions from different library dynamically.

#### [WinSCard Class](src/PcscDotNet/WinSCard.cs "Go to Source")

This class implements [`IPcscProvider`][] using `WinSCard.dll` of *Windows* environment (*Unicode*).

---

Archie Yang
