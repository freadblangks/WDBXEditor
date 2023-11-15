# TODO: Formalize this walkthrough.

User needs to find their my.ini (the default location on Windows seems to be in ProgramData/MySQL/MySQL Server 8.0/)

Then, they need to copy the value for `secure-file-priv` into the ACMIL config.json file.

### TODO
Add logic to Initialize-Acmil to prompt the user for important config values if they aren't set.

Those values should include:
* SecureFilePrivDirectoryAbsolutePath