using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TestAppFromAPB.Interfaces;

namespace TestAppFromAPB.Services;

public class LoggerService : ILogger
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CGetPathesAsync_003Ed__2 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<List<string>> _003C_003Et__builder;

		public LoggerService _003C_003E4__this;

		public int count;

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

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _003CSavePathAsync_003Ed__3 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public LoggerService _003C_003E4__this;

		public string path;

		private TaskAwaiter<string> _003C_003Eu__1;

		private TaskAwaiter _003C_003Eu__2;

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

	public LoggerService()
	{
		throw null;
	}

	[AsyncStateMachine(typeof(_003CGetPathesAsync_003Ed__2))]
	public Task<List<string>> GetPathesAsync(int count)
	{
		throw null;
	}

	[AsyncStateMachine(typeof(_003CSavePathAsync_003Ed__3))]
	public Task SavePathAsync(string path)
	{
		throw null;
	}
}
