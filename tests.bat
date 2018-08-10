cd "%~dp0"
start LameTesting\bin\Release\LameTesting.exe LameTestData\test.wav LameTestData\3.99.5\lame.exe  -V2 LameTestData\3.99.5\test1.mp3 LameTestData\3.100\lame.exe  -V2 LameTestData\3.100\test1.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\test.wav LameTestData\3.99.5\lame.exe  -V5 LameTestData\3.99.5\test2.mp3 LameTestData\3.100\lame.exe  -V5 LameTestData\3.100\test2.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\test.wav LameTestData\3.99.5\lame.exe  -V9 LameTestData\3.99.5\test3.mp3 LameTestData\3.100\lame.exe  -V9 LameTestData\3.100\test3.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\test.wav LameTestData\3.99.5\lame.exe "-b 128" LameTestData\3.99.5\test4.mp3 LameTestData\3.100\lame.exe "-b 128" LameTestData\3.100\test4.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\test.wav LameTestData\3.99.5\lame.exe "-b 320" LameTestData\3.99.5\test5.mp3 LameTestData\3.100\lame.exe "-b 320" LameTestData\3.100\test5.mp3

start LameTesting\bin\Release\LameTesting.exe LameTestData\microtest.wav LameTestData\3.99.5\lame.exe  -V2 LameTestData\3.99.5\microtest1.mp3 LameTestData\3.100\lame.exe  -V2 LameTestData\3.100\microtest1.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\microtest.wav LameTestData\3.99.5\lame.exe  -V5 LameTestData\3.99.5\microtest2.mp3 LameTestData\3.100\lame.exe  -V5 LameTestData\3.100\microtest2.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\microtest.wav LameTestData\3.99.5\lame.exe  -V9 LameTestData\3.99.5\microtest3.mp3 LameTestData\3.100\lame.exe  -V9 LameTestData\3.100\microtest3.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\microtest.wav LameTestData\3.99.5\lame.exe  "-b 128" LameTestData\3.99.5\microtest4.mp3 LameTestData\3.100\lame.exe  "-b 128" LameTestData\3.100\microtest4.mp3
start LameTesting\bin\Release\LameTesting.exe LameTestData\microtest.wav LameTestData\3.99.5\lame.exe  "-b 320" LameTestData\3.99.5\microtest5.mp3 LameTestData\3.100\lame.exe  "-b 320" LameTestData\3.100\microtest5.mp3