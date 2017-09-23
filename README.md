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

This interface declares the properties and methods which needs to be implemented for accessing *PC/SC*.

> The property: `CharacterEncoding`, is used to pass characters with correct encoding while calling specific functions. (e.g., the encoding of reader names, from `SCardListReaders`.)

Currently, this interface declares these methods:

- `SCardEstablishContext`
- `SCardFreeMemory`
- `SCardIsValidContext`
- `SCardReleaseContext`
- **Continue...**

### [Pcsc Class](src/PcscDotNet/Pcsc.cs "Go to Source")

This class is the start point for using `PC/SC` functions. You need to specify [`IPcscProvider`][] instance to create [`Pcsc`][] instance.

### [Pcsc\<TIPcscProvider\> Class](src/PcscDotNet/Pcsc_1.cs "Go to Source")

This class provies static members with corresponding members in [`Pcsc`][] class, using singleton instance of `TIPcscProvider` which implements [`IPcscProvider`][] interface.

> Most of time, `TIPcscProvider` can be used with singleton safely unless its members may be changed, e.g., the provider loads functions from different library dynamically.

#### [WinSCard Class](src/PcscDotNet/WinSCard.cs "Go to Source")

This class implements [`IPcscProvider`][] using `WinSCard.dll` of *Windows* enviroment.

---

Archie Yang