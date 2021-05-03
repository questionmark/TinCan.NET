[![Build Status](https://travis-ci.org/njvsvalhalla/TinCan.NET.svg?branch=master)](https://travis-ci.org/njvsvalhalla/TinCan.NET)

# Info

A .NET Core C#/.NET library for implementing Tin Can API.

# About

This project is a modification of Rustici Software's TinCan.NET Library https://github.com/RusticiSoftware/TinCan.NET and is licensed under the Apache 2.0 license. Also included is modifications from a pull request from github user "peturingi" to update the software for .NET Core, as well as add ActivityDefinition InteractionType. Both of these have not been merged into their master branch as of 8/7/18.
Pull requests included:
https://github.com/RusticiSoftware/TinCan.NET/pull/35 (itself based on : https://github.com/RusticiSoftware/TinCan.NET/pull/17 from 2015, user olivergurnell)
https://github.com/RusticiSoftware/TinCan.NET/pull/34

Modifications done by Neal Daniel (njvsvalhalla):
* Standardized method, field, property, and variable names
* Cleaned up code and made more readable
* Added support for xAPI 1.0.3
* LanguageMap was made with the ability to use an collect initializer
* All calls in RemoteLRS are now async
* The tests were updated to .NET core & fixed any broken tests
* RemoteLRS tests were made async

Changes done by myself:
* Fix for Group object type value. (If we use group in xAPI statement, it fails because of wrong object type value)
* Merged all changes from original TinCan.NET repository upto Release 1.3.0.0

For hosted API documentation, basic usage instructions, supported version listing, etc. visit the main project website at:

http://rusticisoftware.github.io/TinCan.NET/

For more information about the Tin Can API visit:

http://tincanapi.com/

# Installation

Include the TinCan folder in your project and reference the project.

Nuget package located here: https://www.nuget.org/packages/TinCanCore/ 
