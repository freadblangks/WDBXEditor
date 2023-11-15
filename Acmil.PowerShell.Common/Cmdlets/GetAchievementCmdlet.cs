using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Achievements;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Cmdlets
{
	/// <summary>
	/// A PowerShell cmdlet for reading individual Achievements.
	/// </summary>
	[Cmdlet(VerbsCommon.Get, "Achievement")]
	[OutputType(typeof(Achievement))]
	public class GetAchievementCmdlet : BaseCmdlet
	{
		private IAchievementManager _achievementManager;

		/// <summary>
		/// Initializes a new instance of <see cref="GetAchievementCmdlet"/>.
		/// </summary>
		public GetAchievementCmdlet() : base()
		{
			_achievementManager = RootContainer.Resolve<IAchievementManager>();
		}

		[Parameter(Mandatory = true, Position = 0)]
		public ushort AchievementId { get; set; }

		protected override void BeginProcessing()
		{
			base.BeginProcessing();
		}

		protected override void ProcessRecord()
		{
			var result = _achievementManager.ReadAchievement(AchievementId);
			WriteObject(result);
		}
	}
}
