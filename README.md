<<<<<<< HEAD
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://paypal.me/sirokov)
![GitHub release](https://img.shields.io/github/release/romatallinn/unity-puzzlesystem-asset.svg)
![GitHub repo size in bytes](https://img.shields.io/github/repo-size/romatallinn/unity-puzzlesystem-asset.svg)

# Unity Asset: Puzzle System

## Introduction

<span style="display:block;text-align:center; margin:60px;">![Logo](https://raw.githubusercontent.com/romatallinn/unity-puzzlesystem-asset/master/Meta%20Data/large.png?token=AOO4vC_ZA-4J7sn5MqJHwuc_UVEkQLLhks5cm-AFwA%3D%3D)</span>


**Puzzle System** is a highly customizable framework for the projects that contain any kind of puzzles. The framework provides you with the set of common, but yet customizable, solutions; as well as the basic classes that can be used in order to create unique puzzle experiences.



## Table of contents

- [Install](#install)
  - [Unity Asset Package](#unity-asset-package)
  - [Clone Repository](#clone-repository)

- [Get Started](#get-started)
  - [Puzzle Structure](#puzzle-structure)
  - [Editor Utility](#editor-utility)

- [Full Documentation](#full-documentation)

- [Donation](#donation)



## Install

### Unity Asset Package

- Download [PuzzleSystem_v1.unitypackage](https://github.com/romatallinn/unity-puzzlesystem-asset/blob/master/PuzzleSystem_v1.unitypackage)
- Open Unity
- Menu -> Assets -> Import Package -> Custom Package
- Choose the downloaded package, click Open

### Clone Repository

```
// Clone this repository
$ git clone https://github.com/romatallinn/unity-puzzlesystem-asset.git
```



## Get Started

### Puzzle Structure

A very critical to understand in the beginning is how the puzzles will be structured in your project.

There are 3 types of elements for every puzzle:
- **Puzzle Handler** -- responsible for handling situations when the puzzle has been solved or failed. In other words, defines what actions will be taken on these critical events (e.g., solved puzzle -> open gates for the player).

- **Puzzle Logic** -- responsible for defining what kind of logic the puzzle is following; what  player must accomplish in order to solve the puzzle, and what player's actions will lead to the failed condition (e.g., task: activate all triggers).

- **Puzzle Trigger** -- objects that the player will interact with in order to proceed with the puzzle (e.g., press a certain keyboard key in a dedicated zone). It should not contain any logic for puzzle solving. It can, however, contain the information for the Puzzle Logic, where it will be analyzed and the decision will then be given on whether or not this trigger helps to solve the puzzle.

### Editor Utility
There is an editor tool that you can make use of.

In order to open it:
`Menu -> Tools -> Puzzle System`

<span style="display:block;text-align:center;">

![Open Editor Tool](https://blobscdn.gitbook.com/v0/b/gitbook-28427.appspot.com/o/assets%2F-LWSZAoTb0akZrStcZ8k%2F-LWc3kW9MHyHUx43orVx%2F-LWc3nyE4PwJCNn8ArCP%2FScreenshot%202019-01-19%20at%2022.44.35.png?alt=media&token=8feda4cb-b3d8-4b4d-9eb1-d6060407a8f9)

<img src="https://blobscdn.gitbook.com/v0/b/gitbook-28427.appspot.com/o/assets%2F-LWSZAoTb0akZrStcZ8k%2F-LWme9e3zpdRdaVUI-Oh%2F-LWc3t95nIJXM0TV82nF%2FScreenshot%202019-01-19%20at%2022.44.11.png?alt=media&token=1d388944-71b1-4b54-8bcc-87f7e9f11273" width="500px">

</span>



## Full Documentation
You can find an extensive documentation available online at
https://puzzlesystem.gitbook.io.

There you can read more thorough material on all of the aspects and features available in the framework, as well as examples and other tips.



## Donation
I know, this project isn't something huge, but I'm a just passionate student of Computer Science & Engineering that wants to conquer the world with his dev skills. A small tip for a cup of coffee, so I can code all days and nights, would be highly appreciated and really helpful!

<span style="display:block;text-align:center; margin-top:30px;">[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://paypal.me/sirokov)</span>



## License
The MIT License (MIT) 2019 - [Roman Sirokov](https://flist.me/u/rsirokov). Please have a look at the [LICENSE.md]() for more details.
=======
# **_Puzzle System User Guide_**		

  ### **Overview**		
 This User Guide was designed to provide first time users of Puzzle System with a basic overview of the features and functionality of the tool.		

  ### **Installation**		
 The Puzzle System asset requires no installation procedures other than a simple import of the package into the Unity Editor.		

  ### **Quick Start**		
 There are 3 crucial elements:		
 	- Handler component -- defines the actions taken on the lifetime events of the system (On Puzzle Solved or Failed).		
 	- Logic component -- defines the actions that the player must undertake in order to solve or fail the puzzle.		
 	- Trigger component -- defines the needed interaction logic that will trigger the system.		

  The structure of the system is as follows:		
 	- Handler and Logic components are attached to the empty object.		
 	- Trigger objects are stacked as its children.		

  There is a Unity Editor tool (Tools -> Puzzle System) that is intended to simplify the process of the system instantiation; choose desired components and press a button to quickly create a puzzle system.		

  There are also multiple demonstration scenes that can be used for learning purposes. They provide basic setups of the common puzzles.		

  ### **Support & Full Documentation**		
 It is highly recommended to proceed to the full documentation that is available online, [DOCUMENTATION](https://puzzlesystem.gitbook.io). There you will be able to find all needed information for more thorough understanding of the provided asset.		

  For further requests or questions, go to [the Unity Forum Thread](https://forum.unity.com/threads/wip-puzzlesystem-any-ideas.616393/) or [contact me](https://flist.me/u/rsirokov).
>>>>>>> ab4319b1d30f03bffadcc7e95423f3c7b2e001b6
