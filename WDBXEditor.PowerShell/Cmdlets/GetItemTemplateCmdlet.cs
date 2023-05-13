using System.Management.Automation;
using WDBXEditor.Data.Contracts.Models.Items;

namespace Acmil.PowerShell.Cmdlets
{
	[Cmdlet(VerbsCommon.Get, "ItemTemplate")]
	[OutputType(typeof(CompleteItemTemplate))]
	public class GetItemTemplateCmdlet : Cmdlet
	{

	}
}