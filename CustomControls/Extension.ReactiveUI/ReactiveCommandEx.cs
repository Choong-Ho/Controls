using System;
using ReactiveUI;
using System.Reactive;
using System.Collections.Generic;

namespace Extension.ReactiveUI
{
    public static class ReactiveCommandEx
    {
        /* ..WithThrownEx의 loggerAction의 경우 기본으로 설정한 인자 타입은 Exception이다.
         * loggerAction에 추가적인 인자 타입(예외 인자외 다른 인자가 들어가야 한다면..)을 넣어야 한다면
         * 추가적으로 ..WithThrownEx 메소드를 넣도록 한다.
         * ex) CreateAsyncCommandWithThrownEx(Func<IObservable<Unit>> func, Action<Exception, string> loggerAction, IObservable<bool> canExcute = null, IList<IDisposable> disposers = null)
         */

        /// <summary>
        /// 동기 커맨드 생성(With. ThrownExceptions)
        /// 반환값 없는 커맨드 실행 메소드(or Unit -> Unit 리턴 메소드는 궂이 리턴을 하지 않아도 상관이 없음)
        /// </summary>
        /// <param name="action">구현 메소드</param>
        /// <param name="loggerAction">로그(예외) 구현 메소드</param>
        /// <param name="canExcute">커맨드 활성 유무</param>
        /// <param name="disposers">IList<IDisposable> 객체 -> ThrownExceptions.Subscribe()는 IDisposable 을 반환. ThrownExceptions을 구독하는 델리게이트 해제하기 위함.  </param>
        /// <returns></returns>
        public static ReactiveCommand<Unit, Unit> CreateCommandWithThrownEx(Action action, Action<Exception> loggerAction, IObservable<bool> canExcute = null, IList<IDisposable> disposers = null, bool addExcute = true)
        {
            var command = ReactiveCommand.Create(action, canExcute);
            disposers?.Add(command.ThrownExceptions.Subscribe(e => loggerAction(e)));
            if (addExcute) disposers?.Add(command);
            return command;
        }

        /// <summary>
        /// 동기 커맨드 생성(With. ThrownExceptions)
        /// 반환값 없는 커맨드 실행 메소드(or Unit -> Unit 리턴 메소드는 궂이 리턴을 하지 않아도 상관이 없음)
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="action">구현 메소드(파라미터 : TParam)</param>
        /// <param name="loggerAction">로그(예외) 구현 메소드</param>
        /// <param name="canExcute">커맨드 활성 유무</param>
        /// <param name="disposers">IList<IDisposable> 객체 -> ThrownExceptions.Subscribe()는 IDisposable 을 반환. ThrownExceptions을 구독하는 델리게이트 해제하기 위함.  </param>
        /// <returns></returns>
        public static ReactiveCommand<TParam, Unit> CreateCommandWithThrownEx<TParam>(Action<TParam> action, Action<Exception> loggerAction, IObservable<bool> canExcute = null, IList<IDisposable> disposers = null, bool addExcute = true)
        {
            var command = ReactiveCommand.Create(action, canExcute);
            disposers?.Add(command.ThrownExceptions.Subscribe(e => loggerAction(e)));
            if (addExcute) disposers?.Add(command);
            return command;
        }

        /// <summary>
        /// 비동기 커맨드 생성(With. ThrownExceptions)
        /// 커맨드 실행 메소드의 반화 값 IObservable
        /// </summary>
        /// <param name="func">구현 메소드</param>
        /// <param name="loggerAction">로그(예외) 구현 메소드</param>
        /// <param name="canExcute">커맨드 활성 유무</param>
        /// <param name="disposers">IList<IDisposable> 객체 -> ThrownExceptions.Subscribe()는 IDisposable 을 반환. ThrownExceptions을 구독하는 델리게이트 해제하기 위함.  </param>
        /// <returns></returns>
        public static ReactiveCommand<Unit, Unit> CreateAsyncCommandWithThrownEx(Func<IObservable<Unit>> func, Action<Exception> loggerAction, IObservable<bool> canExcute = null, IList<IDisposable> disposers = null, bool addExcute = true)
        {
            var command = ReactiveCommand.CreateFromObservable(func, canExcute);
            disposers?.Add(command.ThrownExceptions.Subscribe(e => loggerAction(e)));
            if (addExcute) disposers?.Add(command);
            return command;
        }

        /// <summary>
        /// 비동기 커맨드 생성(With. ThrownExceptions)
        /// 커맨드 실행 메소드의 반화 값 IObservable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func">구현 메소드(반환값 : TResult)</param>
        /// <param name="loggerAction">로그(예외) 구현 메소드</param>
        /// <param name="canExcute">커맨드 활성 유무</param>
        /// <param name="disposers">IList<IDisposable> 객체 -> ThrownExceptions.Subscribe()는 IDisposable 을 반환. ThrownExceptions을 구독하는 델리게이트 해제하기 위함.  </param>
        /// <returns></returns>
        public static ReactiveCommand<Unit, TResult> CreateAsyncCommandWithThrownEx<TResult>(Func<IObservable<TResult>> func, Action<Exception> loggerAction, IObservable<bool> canExcute = null, IList<IDisposable> disposers = null, bool addExcute = true)
        {
            var command = ReactiveCommand.CreateFromObservable(func, canExcute);
            disposers?.Add(command.ThrownExceptions.Subscribe(e => loggerAction(e)));
            if (addExcute) disposers?.Add(command);
            return command;
        }

        /// <summary>
        /// 비동기 커맨드 생성(With. ThrownExceptions)
        /// 커맨드 실행 메소드의 반화 값 IObservable
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func">구현 메소드(파라미터 : TParam, 반환값 : TResult)</param>
        /// <param name="loggerAction">로그(예외) 구현 메소드</param>
        /// <param name="canExcute">커맨드 활성 유무</param>
        /// <param name="disposers">IList<IDisposable> 객체 -> ThrownExceptions.Subscribe()는 IDisposable 을 반환. ThrownExceptions을 구독하는 델리게이트 해제하기 위함.  </param>
        /// <returns></returns>
        public static ReactiveCommand<TParam, TResult> CreateAsyncCommandWithThrownEx<TParam, TResult>(Func<TParam, IObservable<TResult>> func, Action<Exception> loggerAction, IObservable<bool> canExcute = null, IList<IDisposable> disposers = null, bool addExcute = true)
        {
            var command = ReactiveCommand.CreateFromObservable(func, canExcute);
            disposers?.Add(command.ThrownExceptions.Subscribe(e => loggerAction(e)));
            if (addExcute) disposers?.Add(command);
            return command;
        }
    }
}
