using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Contracts.Types.Primitives;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Cmdlets
{
	// TODO: Make a separeate cmdlet called "GetItemTemplates"
	// TODO: Figure out what we want to do for reading the DBC-side stuff for items.
	/// <summary>
	/// A PowerShell cmdlet for reading individual Item Templates.
	/// </summary>
	[Cmdlet(VerbsCommon.Get, "ItemTemplate")]
	[OutputType(typeof(CompleteItemTemplate))]
	public class GetItemTemplateCmdlet : BaseCmdlet
	{
		private const string PARAMETER_SET_NAME_GET_BY_ENTRY_ID = "ByEntryId";
		private const string PARAMETER_SET_NAME_GET_BY_NAME = "ByName";

		private IItemTemplateManager _itemTemplateManager;

		/// <summary>
		/// Initializes a new instance of <see cref="GetItemTemplateCmdlet"/> class.
		/// </summary>
		public GetItemTemplateCmdlet() : base()
		{
			_itemTemplateManager = RootContainer.Resolve<IItemTemplateManager>();
		}

		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_ENTRY_ID, Position = 0)]
		public UInt24 EntryId { get; set; }

		// TODO: Get rid of this and move its functionality to a new dedicated cmdlet.
		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME, Position = 0)]
		public string Name { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME)]
		public byte Class { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME)]
		public byte SubClass { get; set; }

		protected override void BeginProcessing()
		{
			base.BeginProcessing();
		}

		protected override void ProcessRecord()
		{
			// TODO: Remove this if we don't need it after pulling
			// out the other parameter set.
			if (base.ParameterSetName != PARAMETER_SET_NAME_GET_BY_ENTRY_ID)
			{
				CmdletHelper.HandleUnsupportedParameterSet(this);
			}
			var result = _itemTemplateManager.GetCompleteItemTemplate(EntryId);
			WriteObject(result);
		}
	}
}
