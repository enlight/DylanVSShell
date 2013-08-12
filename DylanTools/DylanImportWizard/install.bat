REM This batch file installs the debug version of the wizard assembly into the GAC.
REM And prints out the public key token.
REM This should be run from within the VS command prompt.
gacutil /if .\bin\Debug\DylanTools.DylanImportWizard.dll
sn -T .\bin\Debug\DylanTools.DylanImportWizard.dll