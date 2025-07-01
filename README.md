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
- [Arguments](#arguments)
	- [`<date-argument>`](#date-argument)
	- [`<query>`](#query)
	- [`<title>`](#title-argument)
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

#### Arguments

- **`<title>`** - The title or description of the task you want to create
- **`[<date-argument>]`** - Optional. When provided, the task will be staged for the specified date. If omitted, the task will be added to your unstaged tasks (backlog). See [`<date-argument>`](#date-argument) for all available date options.

#### Options

- **`--status`, `-s`** - Set the initial status of the task. Accepts `active`, `blocked`, or `completed` (or their shortcuts `a`, `b`, `c`)

##### More about status

TaskStak uses a simple three-state system that reflects real development workflows:

1. **`active`** or **`a`** - Current work you can make progress on (default)
2. **`blocked`** or **`b`** - Work impeded by external dependencies  
3. **`completed`** or **`c`** - Finished work

#### Examples
```bash
task add "implement OAuth2 integration" --today
task add "fix memory leak in parser" -s active
task add "waiting for API keys" -s blocked
task add "update documentation" --status completed
task add "plan database integration" 20250705 
task add "support ticket" --today -s c 
```

### `view`
View tasks staged for specific days using natural language date arguments.

```bash
task view [<date-argument>] [--unstaged | -u] [--verbose | -v]
```

View tasks staged for specific days to see what your upcoming workload is like. Use the date argument to choose specific days to view. See [`<date-argument>`](#date-argument) for more documentation.

#### Arguments

- **`[<date-argument>]`** - Optional. Specifies which date's tasks to view. When omitted, defaults to today's tasks. See [`<date-argument>`](#date-argument) for all available date options.

#### Options

- **`--unstaged`, `-u`** - Show tasks that have not yet been staged to a particular date (i.e., your backlog). Note: This option is mutually exclusive with any date argument. If you specify `--unstaged`, it will ignore any date argument provided.
- **`--verbose`, `-v`** - Show detailed information including task IDs and timestamps. May be used in combination with other arguments or options.

#### Common Uses

**Stak for Today (default)**

When running `task view` with no arguments, displays:
- **Past staged tasks** (in yellow) - Tasks that were staged for previous days appear prominently at the top with a warning to ensure nothing gets lost or forgotten. These are shown in yellow with the number of days since they were originally staged (e.g., "from 3d ago").
- **Active tasks** staged for today
- **Blocked tasks** from any date (keeps dependencies visible and prevents them from being forgotten)  
- **Tasks completed** today

**Staks for Other Dates**

When viewing other dates (e.g. `task view --tomorrow`), displays:
- Tasks staged for that specific day (active)
- If past date, any tasks completed that day

**Unstaged tasks**

Supply the `--unstaged` option to see all tasks that have not yet been staged to any date

#### Examples
```bash
task view                           # Today's push (default)
task view --tomorrow                # Tomorrow's staged tasks
task view --friday                  # Friday's staged tasks
task view 2025-01-15                # Tasks staged for specific date
task view --today --verbose         # Today's tasks with details
task view --unstaged                # Displays any tasks not yet staged to a task push
task view --tomorrow -v             # Tomorrow's tasks with details
```

### `done`
Mark a task as completed.

```bash
task done <query>
```

#### Arguments

- **`<query>`** - Search term to find the task you want to mark as completed. Uses fuzzy search to match task titles. See [`<query>`](#query) for search details.

#### Options

No options available for this command.

#### Examples
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

#### Arguments

- **`<query>`** - Search term to find the task you want to update. Uses fuzzy search to match task titles. See [`<query>`](#query) for search details.

#### Options

- **`--status`, `-s`** - The new status for the task. Accepts `active`, `blocked`, or `completed` (or their shortcuts `a`, `b`, `c`). See [`more about status`](#status) for additional details.

#### Examples
```bash
task move "auth" -s blocked                 # Move auth task to blocked
task move "memory leak" --status active     # Move memory leak task to active
task move "documentation" -s completed      # Mark documentation task as completed
task move "API integration" -s a            # Using status shortcuts
```

### `title`
Modify the title of an existing task.

```bash
task title <query> <title>
```

#### Arguments

- **`<query>`** - Search term to find the task you want to rename. Uses fuzzy search to match task titles. See [`<query>`](#query) for search details.
- **`<title>`** - The new title/description for the task

#### Options

No options available for this command.

#### Examples
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

#### Arguments

- **`<query>`** - Search term to find the task you want to delete. Uses fuzzy search to match task titles. See [`<query>`](#query) for search details.

#### Options

No options available for this command.

#### Examples
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

The date argument provides an intuitive way to stage tasks using natural language, designed to speed up the staging process. You can use the natural language options, aliases for common days, or standard date formats. See [`<date-argument>`](#date-argument). 

#### Arguments

- **`<query>`** - Search term to find the task you want to stage. Uses fuzzy search to match task titles. See [`<query>`](#query) for search details.
- **`[<date-argument>]`** - Optional. Specifies when to stage the task. When omitted, defaults to today. See [`<date-argument>`](#date-argument) for all available date options.

#### Options

- **`--in <number>`** - Stage 'n' days from today; must be a non-negative value. Note: Cannot be combined with the date argument. Use either the date argument or the `--in` option, or neither to default to today's stak.

#### Examples
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

#### Arguments

- **`<query>`** - Search term to find the task you want to unstage. Uses fuzzy search to match task titles. See [`<query>`](#query) for search details.

#### Options

No options available for this command.

#### Examples
```bash
task pop "prepare for sprint planning"   # removes task from staged date

task view --unstaged                     # view unstaged
```

## Arguments

### `<date-argument>`
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

### `<query>`

The `<query>` argument employ fuzzy search to find tasks.

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

### `<title> argument`

The `<title>` argument is used to specify the title or description of a task when adding or renaming tasks. It can be any string that describes the task you want to create or modify. The [`<query>`](#query) argument is used to find tasks using the title as a search term, making titles unique is helpful when updating the tasks further along in your workflow.

## Data Storage

Tasks are stored locally:
- Format JSON
- File path `USER_PATH/.taskStak/tasks.json`
- Copy the file to backup your tasks

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
task push "support ticket"

# During the day
task view                              # Check what's on deck for today
task view --tomorrow                   # Plan tomorrow's work
task view --unstaged                   # Review backlog tasks
task done "registration"               # Mark progress
task move "CSS issues" -s blocked      # Waiting on designer
task pop "code review"                 # Move to backlog, no longer a priority 

# End of the week
task view                              # Review completed tasks for the day
task push "fix bug" --mon              # Push incomplete tasks for the following week

# Clean up
task rm "old prototype"                # Remove obsolete tasks
```

## License

MIT - See LICENSE file for details.

---

**Need help?** Run `task --help` or `task <command> --help` for detailed usage information.