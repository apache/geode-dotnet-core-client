using System;

namespace Apache 
{
    namespace Geode
    {
        namespace DotNetCore
        {
            public abstract class GemfireNativeObject : IDisposable
            {
                private bool _disposed = false;
                protected IntPtr _containedObject;
                
                
                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }

                protected virtual void Dispose(bool disposing)
                {
                    if (_disposed)
                    {
                        return;
                    }

                    if (disposing)
                    {
                        DestroyContainedObject();
                        _containedObject = IntPtr.Zero;
                    }

                    _disposed = true;
                }

                protected abstract void DestroyContainedObject();
            }
        }
    }
}
