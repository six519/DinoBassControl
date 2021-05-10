Dino Bass Controller
====================

Control Google Chrome's T-Rex Game Using Your Bass Guitar. Play the "D" chord following with "G" to jump. Play "E" to quit the application.

Demo
====

https://www.facebook.com/ferdinandsilva/videos/10158814041578290


Install Requirements
====================
::

    nuget install Naudio -Version 1.10.0
    copy NAudio.1.10.0\lib\net35\NAudio.dll

Compile
=======
::

    csc -out:control.exe -win32icon:guitar.ico /r:NAudio.dll *.cs
