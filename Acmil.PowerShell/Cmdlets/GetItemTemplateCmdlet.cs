using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using WDBXEditor.Data.Contracts.Models.Items;

namespace Acmil.PowerShell.Cmdlets
{
	[Cmdlet(VerbsCommon.Get, "ItemTemplate")]
	[OutputType(typeof(CompleteItemTemplate))]
	public class GetItemTemplateCmdlet : Cmdlet
	{

	}
}
