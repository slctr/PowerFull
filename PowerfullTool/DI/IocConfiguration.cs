using Ninject.Modules;
using Powerfull.BLL.Services;
using PowerfullTool.Services;
using PowerfullTool.ViewModels;

namespace PowerfullTool.DI
{
	public class IocConfiguration : NinjectModule
	{
		public override void Load()
		{
			this.Bind<LoanApplicationNotifiedService>().ToSelf().InTransientScope();
			this.Bind<LoanApplicationService>().ToSelf().InTransientScope();
			this.Bind<ApplicationViewModel>().ToSelf().InTransientScope();
		}
	}
}
