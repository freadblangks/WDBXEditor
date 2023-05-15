using Acmil.Data.Contracts.Models.Items;
using Acmil.PowerShell.Common.Helpers;
using Acmil.PowerShell.Common.Helpers.Interfaces;
using Acmil.PowerShell.Common.OutputTypes;
using System.Management.Automation;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Extended.Api.Managers;
using WDBXEditor.Extended.Api.Managers.Interfaces;

namespace Acmil.PowerShell.Common.Cmdlets
{
	// TODO: Make this return an enumerable.
	// TODO: Actually, maybe make a separeate cmdlet called "GetItemTemplates"
	// TODO: Figure out what we want to do for reading the DBC-side stuff for items.
	/// <summary>
	/// A PowerShell cmdlet for reading Item Templates.
	/// </summary>
	[Cmdlet(VerbsCommon.Get, "ItemTemplate")]
	[OutputType(typeof(List<CompleteItemTemplate>))]
	public class GetItemTemplateCmdlet : PSCmdlet
	{
		private const string PARAMETER_SET_NAME_GET_BY_ENTRY_ID = "ByEntryId";
		private const string PARAMETER_SET_NAME_GET_BY_NAME = "ByName";

		private ICmdletHelper _cmdletHelper;
		private IItemTemplateManager _itemTemplateManager;

		/// <summary>
		/// Initializes a new instance of <see cref="GetItemTemplateCmdlet"/> class.
		/// </summary>
		public GetItemTemplateCmdlet() : base()
		{
			_cmdletHelper = new CmdletHelper();
			
		}

		/// <summary>
		/// Initializes a new instance of <see cref="GetItemTemplateCmdlet"/> class..
		/// </summary>
		/// <param name="itemTemplateManager">An implementation of <see cref="IItemTemplateManager"/>.</param>
		internal GetItemTemplateCmdlet(ICmdletHelper cmdletHelper, IItemTemplateManager itemTemplateManager)
		{
			_cmdletHelper = cmdletHelper;
			_itemTemplateManager = itemTemplateManager;
		}

		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_ENTRY_ID, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME, Position = 0)]
		public MySqlConnectionInfo ConnectionInfo { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_ENTRY_ID, Position = 1)]
		public UInt24 EntryId { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME, Position = 1)]
		public string Name { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME)]
		public byte Class { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = PARAMETER_SET_NAME_GET_BY_NAME)]
		public byte SubClass { get; set; }

		protected override void BeginProcessing()
		{
			_itemTemplateManager = new ItemTemplateManager(
				ConnectionInfo.Hostname,
				ConnectionInfo.Credential.UserName,
				ConnectionInfo.Credential.Password
			);
		}

		protected override void ProcessRecord()
		{
			var results = new List<CompleteItemTemplate>();
			switch (base.ParameterSetName)
			{
				case PARAMETER_SET_NAME_GET_BY_ENTRY_ID:
					var result = _itemTemplateManager.GetCompleteItemTemplate(EntryId);
					results = new List<CompleteItemTemplate>() { result };
					break;
				case PARAMETER_SET_NAME_GET_BY_NAME:
					results = _itemTemplateManager.GetCompleteItemTemplates(Name, Class, SubClass);
					break;
				default:
					_cmdletHelper.HandleUnsupportedParameterSet(this);
					break;
			}
			WriteObject(results);
		}
	}
}
