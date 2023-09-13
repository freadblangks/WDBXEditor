using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Contracts.Types.Primitives;
using System.Management.Automation;

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

		///// <summary>
		///// Initializes a new instance of <see cref="GetItemTemplateCmdlet"/> class..
		///// </summary>
		///// <param name="itemTemplateManager">An implementation of <see cref="IItemTemplateManager"/>.</param>
		//internal GetItemTemplateCmdlet(ICmdletHelper cmdletHelper, IItemTemplateManager itemTemplateManager)
		//{
		//	_cmdletHelper = cmdletHelper;
		//	_itemTemplateManager = itemTemplateManager;
		//}


		[Parameter(Mandatory = true, ParameterSetName = PARAMETER_SET_NAME_GET_BY_ENTRY_ID, Position = 0)]
		public UInt24 EntryId { get; set; }

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
			try
			{
				var results = new List<object>();
				switch (base.ParameterSetName)
				{
					case PARAMETER_SET_NAME_GET_BY_ENTRY_ID:
						var result = _itemTemplateManager.GetCompleteItemTemplate(EntryId);
						results = new List<object>() { result };
						break;
					case PARAMETER_SET_NAME_GET_BY_NAME:
						//results = itemTemplateEngine.GetCompleteItemTemplates(Name, Class, SubClass);
						break;
					default:
						CmdletHelper.HandleUnsupportedParameterSet(this);
						break;
				}
				WriteObject(results);
			}
			catch
			{
				throw;
			}
		}
	}
}
