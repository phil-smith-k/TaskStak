# TaskStak

A brutally fast, Git-like command line task management tool built for developers who value speed and simplicity.


- [Installation](#installation)
- [Quick Start](#quick-start)
- [Commands](#commands)
  - [`add`](#add)
  - [`view`](#view)
  - [`done`](#done)
  - [`move`](#move)
  - [`title`](#title)
  - [`rm`](#rm)
  - [`push`](#push)
  - [`pop`](#pop)
- [Task Status](#task-status)
- [Query Argument and Search](#search-system)
- [Data Storage](#data-storage)
- [Example Workflow](#example-workflow)
- [License](#license)

## Installation

Install TaskStak globally using the .NET CLI:

```bash
dotnet tool install --global TaskStak
```

## Quick Start

```bash
# Add your first task
task add "implement user authentication"

# View your tasks
task view

# Mark a task as complete
task done "auth"

# Stage a task for tomorrow
task push "auth" --tomorrow
```

## Commands

### `add`
Add a new task.

```bash
task add <title> [<date-argument>] [--status | -s <active|blocked|completed>]
```

#### Date Argument (optional)
To add a task to specific a day use the date optional date argument. If not supplied, the task will be added to your unstaged tasks. Uses the same natural language patterns as the `push` command. See [`<date-argument>`](#date-argument-date-argument) under the `push` command for complete details on available options.

**Examples:**
```bash
task add "implement OAuth2 integration" --today
task add "fix memory leak in parser" -s active
task add "waiting for API keys" -s blocked
task add "update documentation" --status completed
task add "plan database integration" 20250705 
```

### `view`
View tasks staged for specific days using natural language date arguments.

```bash
task view [<date-argument>] [--unstaged | -u] [--verbose | -v]
```

#### Date Argument
View tasks staged for specific days using the same natural language patterns as the `push` command. See [`<date-argument>`](#date-argument-date-argument) under the `push` command for complete details on available options.

##### Stak for Today (Default)
When running `task view` with no arguments, displays:
- **Past staged tasks** (in yellow) - Tasks that were staged for previous days and need attention
- **Active tasks** staged for today
- **Blocked tasks** from any date (keeps dependencies visible and prevents them from being forgotten)  
- **Tasks completed** today

###### **Past Staged Tasks:**
Tasks that were scheduled for previous days appear prominently at the top with a warning to ensure nothing gets lost or forgotten. These are shown in yellow with the number of days since they were originally staged (e.g., "from 3d ago").

###### Staks for Other Dates
When viewing other dates (ex. `task view --tomorrow`), displays:
- Tasks staged for that specific day (active)
- If past date, any tasks completed that day

#### Unstaged Option
- `--unstaged`, `-u` - Show tasks that have not yet been staged to a particular date (i.e., your backlog) Note: This option is mutually exclusive with any date argument. If you specify `--unstaged`, it will ignore any date argument provided.

#### Verbose Option
- `--verbose`, `-v` - Show detailed information including task IDs and timestamps. May be used in combination with any other argument or option

**Examples:**
```bash
task view                           # Today's push (default)
task view --tomorrow                # Tomorrow's staged tasks
task view --friday                  # Friday's staged tasks
task view 2025-01-15                # Tasks staged for specific date
task view --today --verbose         # Today's tasks with details
task view --unstaged			    # Displays any tasks not yet staged to a task push
task view --tomorrow -v             # Tomorrow's tasks with details
```

### `done`
Mark a task as completed using fuzzy search.

```bash
task done <query>
```

**Examples:**
```bash
task done "auth"                    # Matches "implement user authentication"
task done "memory leak"             # Matches "fix memory leak in parser"
task done "OAuth2 integration"      # Exact phrase matching
```

### `move`
Update the status of an existing task.

```bash
task move <query> [--status | -s <active|blocked|completed>]
```

**Examples:**
```bash
task move "auth" -s blocked                 # Move auth task to blocked
task move "memory leak" --status active     # Move memory leak task to active
task move "documentation" -s completed      # Mark documentation task as completed
task move "API integration" -s a            # Using status shortcuts
```

### `title`
Modify the title of an existing task.

```bash
task title <query> <new-title>
```

**Examples:**
```bash
task title "auth" "implement OAuth2 with refresh tokens"
task title "memory leak" "fix memory leak in JSON parser module"
task title "docs" "update API documentation with examples"
```

### `rm`
Permanently delete a task.

```bash
task rm <query>
```

**Examples:**
```bash
task rm "old feature"               # Remove task permanently
task rm "duplicate task"            # Delete unwanted task
task rm "test"                      # Remove test task
```

**⚠️ Warning:** This action permanently deletes the task and cannot be undone. 

### `push`
Stage tasks for future work on specific days using natural language or date formats.

```bash
task push <query> [<date-argument>] [--in <number>]
```

#### `<date-argument>`
The date argument provides an intuitive way to stage tasks using natural language, designed to speed up the staging process. You can use the natural language options, aliases for common days, or standard date formats.

**Natural Language Options:**
- `--today`, `-t` - Stage for today (default if no argument provided)
- `--tomorrow`, `-tm` - Stage for tomorrow; useful for planning the next day

**Weekday Options:**
- `--monday`, `-mon` - Stage for the next upcoming Monday
- `--tuesday`, `-tue` - Stage for the next upcoming Tuesday
- `--wednesday`, `-wed` - Stage for the next upcoming Wednesday
- `--thursday`, `-thu` - Stage for the next upcoming Thursday
- `--friday`, `-fri` - Stage for the next upcoming Friday
- `--saturday`, `-sat` - Stage for the next upcoming Saturday
- `--sunday`, `-sun` - Stage for the next upcoming Sunday

**Date Formats:**
- `yyyy-MM-dd` - ISO format (e.g., `2025-01-15`)
- `yyyy/MM/dd` - Alternative ISO format (e.g., `2025/01/15`)
- `yyyyMMdd` - Compact format (e.g., `20250115`)
- `yyyy.MM.dd` - Dot notation (e.g., `2025.01.15`)
- `dd-MMM-yyyy` - Month name format (e.g., `15-Jan-2025`)
- Local date formats - Falls back to your system's date parsing for additional flexibility

#### `--in <number>`
- `--in <number>` - Stage 'n' days from today; must be a non-negative value
- **Note:** Cannot be combined with the date argument. Use either the date argument or the `--in` option, or neither to default to today's stak.

**Examples:**
```bash
task push "auth work"                    # Stage for today
task push "code review" --tomorrow       # Stage for tomorrow
task push "team meeting" --friday        # Stage for next Friday
task push "deployment" --in 7            # Stage 7 days from today
task push "release prep" 2025-02-01      # Stage for specific date
task push "planning session" -mon        # Stage for next Monday
```

### `pop`
Unstage tasks from any date, moving them back to your backlog or unstaged tasks.

```bash
task pop <query>
```

**Examples:**
```bash
task pop "prepare for sprint planning"   # removes task from staged date

task view --unstaged                     # view unstaged
```

## Task Status

TaskStak uses a simple three-state system that reflects real development workflows:

1. **`active`** or **`a`** - Current work you can make progress on (default)
2. **`blocked`** or **`b`** - Work impeded by external dependencies  
3. **`completed`** or **`c`** - Finished work

**Status Shortcuts:**
```bash
task add "new feature" -s a              # active
task move "waiting on review" -s b       # blocked  
task move "bug fix" -s c                 # completed
```

## Search System

All commands that use the `<query>` argument employ fuzzy search to find tasks:

**Search Features:**
- **Substring matching**: `"auth"` matches `"implement user authentication"`
- **Multi-word search**: `"memory leak"` matches `"fix memory leak in parser"`
- **Case insensitive**: Works regardless of capitalization
- **ID fallback**: Use 8-character task IDs for exact matching when needed
- **Multiple candidates**: If the query returns multiple tasks, candidates will be displayed with their IDs so you can choose the specific task

**Example of Multiple Candidates:**
```bash
task move 'authentication' --status blocked

⚠ Multiple tasks found. Please be more specific.
[ ] 254F6ABF finish authentication feature               Tue 06/24/2025 07:17 AM
[ ] E117DC73 fix authentication bug in login workflow    Tue 06/17/2025 11:13 PM
```

**Search Tips:**
```bash
# Use quotes for multi-word searches
task done "memory leak"

# Single words work without quotes
task done auth

# Partial matches work well
task move "OAuth" -s blocked
```

## Data Storage

Tasks are stored locally in a human-readable JSON format:
- **Location**: `~/.taskStak/tasks.json`
- **Format**: JSON with full task history and metadata
- **Backup**: Copy the file to backup your tasks
- **Portability**: Works across all platforms where .NET runs

## Example Workflow

```bash
# Monday morning planning
task add "implement user registration API"
task add "fix CSS issues in dashboard" 
task add "code review for authentication PR"

# Organize the week
task push "registration API" --today
task push "CSS issues" --wednesday  
task push "code review" --friday
task push 

# During the day
task view                              # Check what's on deck for today
task view --tomorrow                   # Plan tomorrow's work
task view --unstaged                   # Review backlog tasks
task done "registration"               # Mark progress
task move "CSS issues" -s blocked      # Waiting on designer
task pop "code review"                 # Move to backlog, no longer a priority 

# End of the week
task view							   # Review completed tasks for the day
task push "fix bug" --mon			   # Push incomplete tasks for the following week

# Clean up
task rm "old prototype"                # Remove obsolete tasks
```

## License

MIT - See LICENSE file for details.

---

**Need help?** Run `task --help` or `task <command> --help` for detailed usage information.