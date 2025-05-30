﻿# _n.RoleEditor

This is a tool designed to streamline security role editing in dataverse.

It provides a rich set of features:

- Browsing roles from different environments in a single panel
- Environment > Business Unit > Role hierarchy
- Possibility to search for roles or filter not customizable or managed roles
- Simultaneous editing of multiple roles (even from different environments)
- Docking/undocking role editors to simplify role comparison
- Advanced privilege grouping and filtering
- Possibility to set a single privilege level via "left click" on the privilege cell (increase level by 1)
- Possibility to set a given privilege via "right click" on the privilege cell, showing a contextual menù that presents only the available privilege levels.
- Massive privilege editing "by table"
- Massive privilege editing "by column" (that applies only to visible privileges)
- Copy-Paste privilege configuration
- Possibility to define and apply "privilege set templates" on table-related privileges
- Export in Excel and Markdown
- Export with possibility of reimport after editing
- Possibility to add a role to a given solution 
- Possibility to configure several aspects of the UI (such as icons, default filters, and privilege grouping)
- Contextual detailed help in all screens

## Release 2

- Possibility to search for roles having a given privilege
- Filter role list by solution
- Possibility to configure auto loading of roles when connection changes
- Removed notification when applying snippet
- Possibility to add a privilege to role in bulk
- Possibility to check where a given role is actually used
- Enable/disable request logging

## Release 3

- Compare two roles to show differences
- User browser: show the hierarchy of users and roles assigned to them

## To-do list
This list contains the features that are planned to be implemented in the future.
Order is irrelevant:

- Possibility to assign a given role to a set of users / teams
- Possibility to show the set of permissions assigned to a given user (merging all the roles of the user, and all the roles assigned to user's teams).
- Possibility to export the contents of the change summary in excel or markdown format
