<#
    .SYNOPSIS
    Creates a C# enum in a new file based on the contents of a tab-separated values (TSV) file.

    .DESCRIPTION
    Creates a C# enum in a new file based on the contents of a tab-separated values (TSV) file.
    The TSV should have two columns: The first should be names for the enum values and the
    second should be the numeric IDs that uniquely identify those values. The file should
    not have header row.

    .PARAMETER Filepath
    A path to the TSV file containing the data to build the enum from.

    .PARAMETER EnumName
    The name to give the enum.

    .PARAMETER Namespace
    Optional. A namespace to wrap the enum in. If none is provided, a dummy namespace will be used.

    .PARAMETER DocstringSummaryTemplate
    Optional. A string template defining a <summary> tag to include in a docstring added to each
    enum value. Values will be inserted into the string at indexes containing supported variable
    names inside curly braces (i.e. {supportedVariableName}).

    Here the the variable names supported:
    * nameRaw - The value in the supplied TSV file's first column.
    * name - The name of the enum value to be saved to the output file.
    * identifier - The numeric value supplied in the TSV file's second column.

    .PARAMETER DocstringRemarksTemplate
    Optional. A string template defining a <remarks> tag to include in a docstring added to each
    enum value. Values will be inserted into the string at indexes containing supported variable
    names inside curly braces (i.e. {supportedVariableName}).

    Here the the variable names supported:
    * nameRaw - The value in the supplied TSV file's first column.
    * name - The name of the enum value to be saved to the output file.
    * identifier - The numeric value supplied in the TSV file's second column.

    NOTE: This parameter can only be used if a value was provided for the docstring summary as well.

    .INPUTS
    None. You cannot pipe objects to this cmdlet.

    .OUTPUTS
    None. A file whose name is the value provided for the EnumName parameter with a *.cs extension is written
    to the ConvertedEnums folder in the same directory as this script.

    .EXAMPLE
    PS> .\ConvertFrom-TabSeparatedNamesAndIdsToCSharpEnum.ps1 -Filepath test.tsv -EnumName "WmoAreaId"

    .EXAMPLE
    PS> ConvertFrom-TabSeparatedNamesAndIdsToCSharpEnum -Filepath test.tsv -EnumName "WmoAreaId"

    .EXAMPLE
    PS> .\ConvertFrom-TabSeparatedNamesAndIdsToCSharpEnum.ps1 -Filepath test.tsv -EnumName "WmoAreaId" -Namespace "Acmil.Data.Contracts.Models.Achievements.Enums"

    .EXAMPLE
    PS> .\ConvertFrom-TabSeparatedNamesAndIdsToCSharpEnum.ps1 -Filepath test.tsv -EnumName "WmoAreaId" -DocstringSummaryTemplate "The WorldMapOverlay (WMO) ID for {nameRaw}"

    .EXAMPLE
    PS> .\ConvertFrom-TabSeparatedNamesAndIdsToCSharpEnum.ps1 -Filepath test.tsv -EnumName "WmoAreaId" -DocstringSummaryTemplate "The WorldMapOverlay (WMO) ID for {nameRaw}." -DocstringRemarksTemplate "I think it's super interesting that the value is {identifier}."
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory=$True)]
    [ValidateNotNullOrEmpty()]
    [string] $Filepath,

    [Parameter(Mandatory=$True)]
    [ValidateNotNullOrEmpty()]
    [string] $EnumName,

    [Parameter(Mandatory=$False)]
    [string] $Namespace = "",

    [Parameter(Mandatory=$False)]
    [string] $DocstringSummaryTemplate = "",

    [Parameter(Mandatory=$False)]
    [string] $DocstringRemarksTemplate = ""
)

$CHARACTERS_TO_REMOVE_PATTERN = "[ '`"``\-?!\^\$#*@~´‘’`“`”]";
$CHARACTERS_TO_REPLACE_WITH_UNDERSCORES_PATTERN = "(?: - )|[:\(\),\[\]\{\}]";
$CHARACTERS_TO_REPLACE_WITH_AND_PATTERN = "[&]";
$CHARACTERS_TO_REPLACE_WITH_PLUS_PATTERN = "[+]";
$CHARACTERS_TO_REPLACE_WITH_PERCENT_PATTERN = "[%]";
$CHARACTERS_TO_REPLACE_WITH_EQUALS_PATTERN = "[=]";
$CHARACTERS_TO_REPLACE_WITH_SLASH_PATTERN = "[\\/]";
$CHARACTERS_TO_REPLACE_
$DUMMY_NAMESPACE = "Dummy.Namespace.Please.Replace";

$OUTPUT_DIRECTORY = "ConvertedEnums";
$OUTPUT_PATH = "$OUTPUT_DIRECTORY\$EnumName.cs";

function script:ConvertTo-ValidEnumName($nameRaw, [ref]$numberEncounteredAtStartOfNameFlag) {
    
    $cleanedName = $nameRaw;

    # We can't have a numeric character at the start of an enum value name.
    # Prepend an underscore to make it valid.
    if ($nameRaw -match '^\d') {
        $numberEncounteredAtStartOfNameFlag.Value = $True;
        $cleanedName = "_$nameRaw";
    }

    $cleanedName = $cleanedName `
        -Replace $CHARACTERS_TO_REPLACE_WITH_UNDERSCORES_PATTERN,"_"    <# NOTE: This needs to come before the pattern that replaces spaces. #>   `
        -Replace $CHARACTERS_TO_REMOVE_PATTERN,""   `
        -Replace $CHARACTERS_TO_REPLACE_WITH_AND_PATTERN,"And"  `
        -Replace $CHARACTERS_TO_REPLACE_WITH_PLUS_PATTERN,"Plus"    `
        -Replace $CHARACTERS_TO_REPLACE_WITH_PERCENT_PATTERN,"Percent"  `
        -Replace $CHARACTERS_TO_REPLACE_WITH_EQUALS_PATTERN,"Equals"  `
        -Replace $CHARACTERS_TO_REPLACE_WITH_SLASH_PATTERN,"Slash"  `

    return $cleanedName;
}

function script:Test-IsValueIdValid($valueId) {
    return $valueId -is [byte]  `
        -or $valueId -is [sbyte]    `
        -or $valueId -is [Int16]    `
        -or $valueId -is [Int32]    `
        -or $valueId -is [Int64];
}

function script:Get-EnumValueDeclarationString($valueName, $valueIdentifier, $isLastValue) {
    $valueDeclarationString = $valueName;

    $valueDeclarationString += " = $valueIdentifier";
    if (-not $isLastValue) {
        $valueDeclarationString += ",";
    }

    return $valueDeclarationString;
}

function script:Get-DocstringText($nameRaw, $name, $identifier, $templateString) {
    return $templateString  `
        -Replace "{nameRaw}",$nameRaw   `
        -Replace "{name}",$name `
        -Replace "{identifier}","$identifier";
}

$isDocstringSummaryTemplateProvided = -not [string]::IsNullOrWhiteSpace($DocstringSummaryTemplate);
$isDocstringRemarksTemplateProvided = -not [string]::IsNullOrWhiteSpace($DocstringRemarksTemplate);
if (-not $isDocstringSummaryTemplateProvided -and $isDocstringRemarksTemplateProvided) {

    # This is because we never want a docstring with a <remarks> tag unless we have a <summary> tag.
    # Documenting a remark when there's no summary doesn't make a ton of sense, and it makes me sad.
    throw "The DocstringRemarksTemplate parameter cannot be used unless a value was also provided for the DocstringSummaryTemplate parameter."
}

if ([string]::IsNullOrWhiteSpace($Namespace)) {
    $Namespace = $DUMMY_NAMESPACE;
}

$rows = Get-Content -Path $Filepath -Encoding utf8;

if ($null -eq $rows -or $rows.Length -eq 0) {
    throw "No data read from file at path '$Filepath'.";
}

$numberEncounteredAtStartOfName = $False;
$rowObjects = $rows `
    | ConvertFrom-String -Delimiter "\t" -PropertyNames NameRaw,ValueId `
    | ForEach-Object {
        $_ | Add-Member -TypeName "string" -NotePropertyName "Name" -NotePropertyValue (ConvertTo-ValidEnumName -nameRaw $_.NameRaw -numberEncounteredAtStartOfNameFlag ([ref]$numberEncounteredAtStartOfName) ) -PassThru
    };

# Validate value IDs.
foreach ($rowObject in $rowObjects) {
    if (-not (Test-IsValueIdValid -valueId $rowObject.ValueId)) {
        throw "Invalid enum value '$($rowObject.ValueId)' found associated with name '$($rowObject.NameRaw)'."
    }
}

# Create the output directory if it doesn't already exist.
if (-not (Test-Path $OUTPUT_DIRECTORY -PathType Container)) {
    New-Item -ItemType Directory -Force -Path $OUTPUT_DIRECTORY | Out-Null;
}

# Create the output file, overwriting any existing file with the same name.
"namespace $Namespace" | Out-File -FilePath $OUTPUT_PATH -Force;
"{" | Out-File -FilePath $OUTPUT_PATH -Append;  # Namespace opening brace.

"`tpublic enum $EnumName" | Out-File -FilePath $OUTPUT_PATH -Append;
"`t{" | Out-File -FilePath $OUTPUT_PATH -Append;    # Enum opening brace.

$emptyCounter = 0;
for ($i = 0; $i -lt $rowObjects.length; ++$i) {

    # If we get a value whose parsed name is empty, replace it with placeholder text.
    $valueName = $rowObjects[$i].Name;
    if ([string]::IsNullOrWhiteSpace($valueName)) {
        $valueName = "EMPTY$($emptyCounter++)";
    }
    $isLastRow = $i -eq ($rowObjects.length - 1);

    if ($isDocstringSummaryTemplateProvided) {
        $summaryText = Get-DocstringText -nameRaw $rowObjects[$i].NameRaw -name $valueName -identifier $rowObjects[$i].ValueId -templateString $DocstringSummaryTemplate
        "`t`t/// <summary>" | Out-File -FilePath $OUTPUT_PATH -Append;
        "`t`t/// $summaryText" | Out-File -FilePath $OUTPUT_PATH -Append;
        "`t`t/// </summary>" | Out-File -FilePath $OUTPUT_PATH -Append;
    }

    if ($isDocstringRemarksTemplateProvided) {
        $remarksText = Get-DocstringText -nameRaw $rowObjects[$i].NameRaw -name $valueName -identifier $rowObjects[$i].ValueId -templateString $DocstringRemarksTemplate
        "`t`t/// <remarks>" | Out-File -FilePath $OUTPUT_PATH -Append;
        "`t`t/// $remarksText" | Out-File -FilePath $OUTPUT_PATH -Append;
        "`t`t/// </remarks>" | Out-File -FilePath $OUTPUT_PATH -Append;
    }

    Write-Host $rowObjects[$i].ValueId.getType();
    $valueDeclaration = Get-EnumValueDeclarationString -valueName $valueName -valueIdentifier $rowObjects[$i].ValueId -isLastValue $isLastRow;
    "`t`t$valueDeclaration" | Out-File -FilePath $OUTPUT_PATH -Append;

    if ($isDocstringSummaryTemplateProvided -and -not $isLastRow) {
        "`r`n" | Out-File -FilePath $OUTPUT_PATH -Append -NoNewline;   # Write extra line between values.
    }
}

"`t}" | Out-File -FilePath $OUTPUT_PATH -Append;    # Enum closing brace.
"}" | Out-File -FilePath $OUTPUT_PATH -Append;  # Namespace closing brace.

if ($emptyCounter -gt 0) {
    Write-Warning "Empty values were encountered in the supplied TSV file. They were written to the output file using placeholder names which begin with 'EMPTY'.";
}

if ($numberEncounteredAtStartOfName) {
    Write-Warning "One or more enum names in the supplied TSV file began with a numeric character. An underscore was prepended to these names before writing them to the output file.";
}
