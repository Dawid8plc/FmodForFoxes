﻿using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using System;

// DO NOT include FMOD namespace in ANY of your classes.
// Use FMOD.SomeClass instead.
// FMOD classes seriously interfere with System namespace.

namespace ChaiFoxes.FMODAudio.Studio
{
	/// <summary>
	/// Bus class.
	/// Represents a global mixer bus within FMOD Studio.
	/// </summary>
	public class Bus
	{
		protected FMOD.Studio.Bus _bus { get; private set; }

		/// <summary>
		/// Bus target volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// -1 - Inverted Signal.
		/// This ignores modulation / automation applied to the volume within Studio.
		/// </summary>
		public float Volume
		{
			get
			{
				_bus.getVolume(out var volume);
				return volume;
			}
			set =>
				_bus.setVolume(value);
		}

		/// <summary>
		/// Bus current volume.
		/// 1 - Normal volume.
		/// 0 - Muted.
		/// -1 - Inverted Signal.
		/// This takes into account modulation / automation applied to the volume within Studio.
		/// </summary>
		public float CurrentVolume
		{
			get
			{
				_bus.getVolume(out _, out var finalVolume);
				return finalVolume;
			}
		}

		/// <summary>
		/// Tells if the bus is muted.
		/// Muting a bus will override its inputs,
		/// but they will obey their individual mute states once it is unmuted.
		/// </summary>
		public bool Muted
		{
			get
			{
				_bus.getMute(out var mute);
				return mute;
			}
			set =>
				_bus.setMute(value);
		}

		/// <summary>
		/// The bus' core channel group.
		/// Please be aware that, by default, the channel group will be deleted when it isn't needed.
		/// </summary>
		public FMOD.ChannelGroup ChannelGroup
		{
			get
			{
				_bus.getChannelGroup(out FMOD.ChannelGroup channelGroup);
				return channelGroup;
			}
		}

		/// <summary>
		/// Tells if the bus is paused.
		/// Pausing a bus will override its inputs,
		/// but they will obey their individual playback states once it is unpaused.
		/// </summary>
		public bool Paused
		{
			get
			{
				_bus.getPaused(out var paused);
				return paused;
			}

			set =>
				_bus.setPaused(value);
		}

		/// <summary>
		/// The Bus's internal path, i.e. "bus:/SFX/Ambience".
		/// </summary>
		public string Path
		{
			get
			{
				_bus.getPath(out string path);
				return path;
			}
		}

		/// <summary>
		/// The Bus's GUID.
		/// </summary>
		public Guid ID
		{
			get
			{
				_bus.getID(out Guid id);
				return id;
			}
		}

		public Bus(FMOD.Studio.Bus bus)
		{
			_bus = bus;
		}

		/// <summary>
		/// Forces FMOD to create the channel group if it doesn't exist yet,
		/// as well as forcing it to stay loaded.
		/// </summary>
		public void LockChannelGroup() =>
			_bus.lockChannelGroup();

		/// <summary>
		/// Allows FMOD to destroy the channel group when it isn't needed.
		/// </summary>
		public void UnlockChannelGroup() =>
			_bus.unlockChannelGroup();

		/// <summary>
		/// Stop all events routed to the bus.
		/// </summary>
		public void StopAllEvents(bool immediate = false) =>
			_bus.stopAllEvents(immediate ? FMOD.Studio.STOP_MODE.IMMEDIATE : FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
}
