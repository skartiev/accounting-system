﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AccountingSystem.Service
{
    public abstract class NotifyPropertyChanges : Disposable, INotifyPropertyChanged
    {
        #region Public Events
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }

        #endregion

        #region Private Events
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        private event PropertyChangedEventHandler propertyChanged;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the when property changed observable event. Occurs when a property value changes.
        /// </summary>
        /// <value>
        /// The when property changed observable event.
        /// </value>
        public IObservable<string> WhenPropertyChanged
        {
            get
            {
                ThrowIfDisposed();
                return Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => propertyChanged += h,
                        h => propertyChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }

        #endregion
        #region Protected Methods
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Assert(
                string.IsNullOrEmpty(propertyName) ||
                (GetType().GetRuntimeProperty(propertyName) != null),
                "Check that the property name exists for this instance.");
            var eventHandler = propertyChanged;
            eventHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            Debug.Assert(string.IsNullOrEmpty(propertyName) || (GetType().GetRuntimeProperty(propertyName) != null),  "Check that the property name exists for this instance.");
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        protected void OnPropertyChanging(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanging(propertyName);
            }
        }
        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(ref TProp currentValue, TProp newValue, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();
            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyName);
                currentValue = newValue;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyNames">The names of all properties changed.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(ref TProp currentValue, TProp newValue, params string[] propertyNames)
        {
            ThrowIfDisposed();
            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyNames);
                currentValue = newValue;
                OnPropertyChanged(propertyNames);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(Func<bool> equal, Action action, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();
            if (equal())
            {
                return false;
            }
            OnPropertyChanging(propertyName);
            action();
            OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(Func<bool> equal, Action action, params string[] propertyNames)
        {
            ThrowIfDisposed();
            if (equal())
            {
                return false;
            }
            OnPropertyChanging(propertyNames);
            action();
            OnPropertyChanged(propertyNames);
            return true;
        }
        #endregion
    }
}
