using System;
using Rondo.Core.Lib;
using Rondo.Core.Lib.Platform;
using Rondo.Core.Memory;

namespace Rondo.Core.Cmds {
    public static unsafe class Time {
        public static Cmd Now<TMsg>(CLf<DateTime, TMsg> toMsg)
                where TMsg : unmanaged {
            static void Impl(Ptr pPayload, CLf<Ptr, Ptr> toMsg, PostMessage post) {
                post.Invoke(toMsg, pPayload);
                toMsg.Dispose();
            }

            return Cmd.New(&Impl, toMsg, DateTime.Now);
        }
    }
}