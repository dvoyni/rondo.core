using System.Collections.Generic;

namespace Rondo.Core.Memory {
    public sealed class RefHash {
        private readonly List<object> _objects = new() { null };
        private readonly Dictionary<object, Ref> _objectToHashed = new();

        public void Clear() {
            _objects.Clear();
            _objects.Add(null);
            _objectToHashed.Clear();
        }

        public void Remove(Ref r) {
            var obj = _objects[r.GetHashCode()];
            _objectToHashed.Remove(obj);
            _objects[r.GetHashCode()] = null;
        }

        public T Remove<T>(Ref r) where T : class {
            var obj = _objects[r.GetHashCode()];
            _objectToHashed.Remove(obj);
            _objects[r.GetHashCode()] = null;
            return (T)obj;
        }

        public Ref Hash(object obj) {
            //TODO: pass type to Ref and (1) check before cast in Get<T>() (2) make TryGet<T>()

            if (ReferenceEquals(obj, null)) {
                return Ref.Null;
            }

            if (!_objectToHashed.TryGetValue(obj, out var o)) {
                o = new Ref(_objects.Count);

                _objects.Add(obj);
                _objectToHashed.Add(obj, o);
            }

            return o;
        }

        public T Get<T>(Ref r) where T : class {
            return (T)_objects[r.GetHashCode()];
        }

#if DEBUG
        public override string ToString() {
            return $"{nameof(RefHash)}(Objects:{_objects.Count})";
        }
#endif
    }
}