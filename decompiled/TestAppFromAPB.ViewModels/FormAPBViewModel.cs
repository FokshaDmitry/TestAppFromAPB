using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TestAppFromAPB.Enums;
using TestAppFromAPB.Interfaces;
using TestAppFromAPB.Models;

namespace TestAppFromAPB.ViewModels;

public class FormAPBViewModel
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CChangeFilter_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

		public FormAPBViewModel _003C_003E4__this;

		public FilterMethod filter;

		private void MoveNext()
		{
			throw null;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			throw null;
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CParceFile_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

		public bool addFile;

		public FormAPBViewModel _003C_003E4__this;

		public string Path;

		public FilterMethod filter;

		private TaskAwaiter<string> _003C_003Eu__1;

		private void MoveNext()
		{
			throw null;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			throw null;
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public List<APBFileModel> fileModels;

	public IFilePicker filePicker;

	public ILogger logger;

	public FormAPBViewModel()
	{
		throw null;
	}

	[AsyncStateMachine(typeof(_003CChangeFilter_003Ed__4))]
	public Task<string> ChangeFilter(FilterMethod filter)
	{
		throw null;
	}

	[AsyncStateMachine(typeof(_003CParceFile_003Ed__5))]
	public Task<string> ParceFile(string Path, FilterMethod filter, bool addFile = false)
	{
		throw null;
	}
}
