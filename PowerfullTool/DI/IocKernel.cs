using Ninject;
using Ninject.Modules;

namespace PowerfullTool.DI
{
	public static class IocKernel
	{
		private static StandardKernel _kernel;

		public static TObject Get<TObject>()
		{
			return _kernel.Get<TObject>();
		}

		public static void Initialize(params INinjectModule[] modules)
		{
			if (_kernel == null)
			{
				_kernel = new StandardKernel(modules);
			}
		}
	}
}
