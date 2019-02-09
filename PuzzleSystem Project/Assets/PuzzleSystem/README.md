# **_Puzzle System User Guide_**

#### **Overview**
This User Guide was designed to provide first time users of Puzzle System with a basic overview of the features and functionality of the tool.

#### **Installation**
The Puzzle System asset requires no installation procedures other than a simple import of the package into the Unity Editor.

#### **Quick Start**
There are 3 crucial elements:
	- Handler component -- defines the actions taken on the lifetime events of the system (On Puzzle Solved or Failed).
	- Logic component -- defines the actions that the player must undertake in order to solve or fail the puzzle.
	- Trigger component -- defines the needed interaction logic that will trigger the system.

The structure of the system is as follows:
	- Handler and Logic components are attached to the empty object.
	- Trigger objects are stacked as its children.

There is a Unity Editor tool (Tools -> Puzzle System) that is intended to simplify the process of the system instantiation; choose desired components and press a button to quickly create a puzzle system.

There are also multiple demonstration scenes that can be used for learning purposes. They provide basic setups of the common puzzles.

#### **Support & Full Documentation**
It is highly recommended to proceed to the full documentation that is available online, [DOCUMENTATION](https://puzzlesystem.gitbook.io). There you will be able to find all needed information for more thorough understanding of the provided asset.

For further requests or questions, go to [the Unity Forum Thread](https://forum.unity.com/threads/wip-puzzlesystem-any-ideas.616393/) or [contact me](https://flist.me/u/rsirokov).