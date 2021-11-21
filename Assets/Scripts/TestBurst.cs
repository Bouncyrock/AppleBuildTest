using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TestBurst : MonoBehaviour
{
    JobHandle _lastJh;
    NativeArray<uint> _vals;

    void OnEnable()
    {
        _vals = new NativeArray<uint>(1000, Allocator.Persistent);
    }

    void OnDisable()
    {
        if (_vals.IsCreated)
        {
            _vals.Dispose();
            _vals = default;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _lastJh = new TestJob()
        {
            TestVals = _vals
        }.Schedule(_lastJh);
        _lastJh.Complete();
    }

    struct TestJob : IJob
    {
        public NativeArray<uint> TestVals;

        public void Execute()
        {
            for (var i = 0; i < TestVals.Length; i++)
            {
                TestVals[i] += 1;
            }
        }
    }
}
