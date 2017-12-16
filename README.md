# TrueColorConsole
24-bit color console for .NET !

[![NuGet](https://img.shields.io/badge/nuget-v1.0.1-blue.svg)](https://www.nuget.org/packages/TrueColorConsole/)

## Synopsis

The feature was announced in [24-bit Color in the Windows Console!](https://blogs.msdn.microsoft.com/commandline/2016/09/22/24-bit-color-in-the-windows-console/), now you too can enjoy it in your .NET console app.

And as a bonus, a high-throughput mode has been implemented for demanding apps (see gallery).

## Gallery

![](https://github.com/aybe/TrueColorConsole/raw/master/example1.png)


![](https://github.com/aybe/TrueColorConsole/raw/master/example3.png)

[Watch a video of the above plasma running at 50 FPS in the console :)](https://github.com/aybe/TrueColorConsole/raw/master/example3.webm)

(if your browser fails to play it, download it and use something like VLC)

## Notes

1. check the [official docs](https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences) for complete reference
2. everything has been implemented but regular attributes and modifiers for input sequences
3. it is *mostly* documented, refer to #1 as necessary
4. you won't explicitly have to deal with VT sequences, but it does not entirely wrap good ol' `Console`
5. basic functionality has been tested but complex VT scenarios haven't
6. see *TrueColorConsoleApp* project for examples

## Contribute

What you can do:

- missing documentation
- nice examples !
- extend public API 
- testing
- report bugs

## Credits

plasma code taken from [libcaca](http://caca.zoy.org/wiki/libcaca) 

## Changes

1.0.1

- full docs
- optimization and fixes
- all formatting features
  - but [Foreground|Background]Extended, these stay as helper methods since usage is simpler that way
- support of non Win10 Anniversary Update systems
