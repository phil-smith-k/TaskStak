# TaskStak

TaskStak - A developer-focused, performant task management CLI tool.

## Installation

```bash
dotnet tool install --global TaskStak
```

## Usage

### Core Commands

#### Add
Add a task to your stak.
```
add <title> [-s|--status <active | blocked | completed>]
```

#### Done
Mark a task complete.
```
done <query>
```

#### Move
Update the status of a task.
```
move <query> [-s|--status <active | blocked | completed>]  
```

#### Title
Update the title of an existing task.
```
title <query> <newTitle>
```

#### View
View your tasks
```
view [-v|--view <day | verbose>]     
```

### Task Status

Tasks fall into one of three states:
1. `active` or `a` - current work (default) 
2. `blocked` or `b` - impeded; waiting on dependencies 
3. `completed` or `c` - finished work 

```bash
# Shortcut usage (moves task to blocked status)
task move "fix bug in auth service" -s b
```

### View Options

#### Daily View (default):
Displays tasks in three sections; active, blocked (if any), and completed

```
task view 
task view -v day
task view -v d
```

#### Verbose view:
Displays more information (key, title, exact date/time)
```
task view -v v
task view -v verbose
```

### Search

All commands use fuzzy search to find tasks by title. Use quotes for multi-word searches and titles.

## Data Storage

Tasks are stored in `~/.taskStak/tasks.json`

## License

MIT