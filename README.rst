========================================
Dylan for Visual Studio (Isolated) Shell
========================================

The aim of this project is to build a modern Visual Studio powered IDE for the
`Dylan programming language <http://opendylan.org/>`_. Microsoft Visual Studio
can be extended to support new programming languages, however, extensions
can't be installed in the Express editions of the Visual Studio line. 
Fortunately, Microsoft also provides the free Visual Studio Shell, that can 
be extended as needed.

There are two versions of the shell, integrated and isolated. Extensions built
for the integrated shell are automatically merged with any other edition of 
Visual Studio you may have installed, however, the integrated shell is 
incompatible with the Express editions. The isolated shell on the other hand
can run side-by-side with any other editions of Visual Studio you may have
installed.

Compiling
=========

Requirments
-----------
* `Visual Studio 2012 SDK <https://www.microsoft.com/en-au/download/details.aspx?id=30668>`_
* Visual Studio 2012
