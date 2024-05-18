using System;
using System.Collections;
using UnityEngine;

namespace ChronoArkMod.Helper;

internal static class DeferredCoroutine
{
    internal static Coroutine StartDeferredCoroutine(this MonoBehaviour instance, Action action, int frames = 1)
    {
        return instance.StartCoroutine(DeferredFrames(action, frames));
    }

    internal static Coroutine StartDeferredCoroutine(this MonoBehaviour instance, Action action, float seconds)
    {
        return instance.StartCoroutine(DeferredSeconds(action, seconds));
    }

    internal static Coroutine StartDeferredCoroutine(this MonoBehaviour instance, Action action, Func<bool> predicate)
    {
        return instance.StartCoroutine(DeferredAwaiter(action, predicate));
    }

    private static IEnumerator DeferredFrames(Action action, int frames = 1)
    {
        for (int i = 0; i < frames; ++i) {
            yield return null;
        }
        action();
    }

    private static IEnumerator DeferredSeconds(Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    private static IEnumerator DeferredAwaiter(Action action, Func<bool> predicate)
    {
        yield return new WaitUntil(predicate);
        action();
    }
}
