using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TestAppFromAPB.Interfaces;

namespace TestAppFromAPB.Services;

public class FilePickerService : IFilePicker
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CGetFileAsync_003Ed__0 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

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
	private struct _003CSaveFileAsync_003Ed__1 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public string fileText;

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

	[AsyncStateMachine(typeof(_003CGetFileAsync_003Ed__0))]
	public Task<string> GetFileAsync()
	{
		throw null;
	}

	[AsyncStateMachine(typeof(_003CSaveFileAsync_003Ed__1))]
	public Task SaveFileAsync(string fileText)
	{
		throw null;
	}

	public FilePickerService()
	{
		throw null;
	}
}
