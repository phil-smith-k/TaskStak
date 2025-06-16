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
done  <query>
```

#### Move
Update the status of a task.
```
move <query> [-s|--status <active | blocked | completed>]  
```

#### Title
Update the title of an existing task.
```
title <query> <title>
```

#### View
View your tasks
```
view [-v|--view <day | verbose>]     
```

```bash
# Add a new task
task add "implement user authentication"

# View tasks
task view

# Complete a task
task done "auth"

# Edit task title
task title "auth" "implement OAuth2 authentication"

# Move task to different status
task move "auth" -s blocked
task move "auth" -s completed
```

### Task Status

Tasks fall into one of three states:
| Status        | Default       | Usage                            | Shortcut    |
| ------------- | ------------- | ---------------------------------|-------------|
| `active`      | YES           | Current work                     | `a`         |
| `blocked`     | NO            | Impeded, waiting on dependencies | `b`         |
| `completed`   | NO            | Finished work                    | `c`         |


```bash
# Shortcut Usage (moves task to blocked status)

task move "fix bug in auth service" -s b
```

### View Options

#### Daily View
Default view:

```
task view 
task view -v day
task view -v d
```

Verbose view:
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