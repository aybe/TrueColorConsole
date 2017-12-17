# TrueColorConsole

24-bit coloring and VT features for .NET System.Console !

[![NuGet](https://img.shields.io/badge/nuget-v1.0.1-blue.svg)](https://www.nuget.org/packages/TrueColorConsole/)

## Synopsis

The feature was announced in [24-bit Color in the Windows Console!](https://blogs.msdn.microsoft.com/commandline/2016/09/22/24-bit-color-in-the-windows-console/), now you too can enjoy it in your .NET console app.

Additionally, a high-throughput mode has been implemented for demanding apps (see gallery).

## Gallery

![](https://github.com/aybe/TrueColorConsole/raw/master/example1.png)


![](https://github.com/aybe/TrueColorConsole/raw/master/example3.png)

[Watch a video of the above plasma running at 50 FPS in the console :)](https://github.com/aybe/TrueColorConsole/raw/master/example3.webm)

(if your browser fails to play it, download it and use something like VLC)

## Notes

See the [official docs](https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences) for complete reference.

Modifiers for input sequences have not been implemented, yet.

Basic functionality has been tested, advanced VT features haven't been.

The `VTConsole` class enables the features along a few helper methods for settings color and such, but it does not wrap `Console` class entirely as it's pretty big. Actually you won't need at all to change your existing code based on `Console`, just invoke `VTConsole` functions you need before/after your calls to regular console, e.g. set a 24-bit color with `VTConsole` and write formatted lines with `Console`.

## Contribute

What you can do:

Share with us a link to your website showing your slick console-based game or any other type of application leveraging VT features, we'll add it here.

The public API could be extended, especially the `Write...` methods as currently only overloads with `string` are available. We therefore decided to keep it slim for the time being and at the same time not push users to deprecate `Console` which is absolutely not. Feedback and time will tell as on the where public API will go, you can open an issue and discuss about the subject.

Report bugs, ensure that the behavior you're encountering is not intentional by checking the official docs first.

## Credits

plasma code taken from [libcaca](http://caca.zoy.org/wiki/libcaca) 

## Changes

1.0.2
 - better readme and description
 
1.0.1

- full docs
- optimization and fixes
- all formatting features but [Foreground|Background]Extended, these are as simpler to use helper methods
- support of non Win10 Anniversary Update systems
