#!/bin/bash
set -e

# ============================================
# RocketDotnet Setup Script
# Creates or updates a .NET solution with Domain, DAL, BL, and UI projects
# ============================================

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# --help output
if [ "$1" = "--help" ] || [ "$1" = "-h" ]; then
  echo "Usage: $0 <SolutionName> [options]"
  echo ""
  echo "Creates a new .NET solution with the following structure:"
  echo "  - Domain: Class library for core entities"
  echo "  - DAL: Data access layer (depends on Domain)"
  echo "  - BL: Business logic layer (depends on Domain and DAL)"
  echo "  - UI: Console application (depends on BL and Domain)"
  echo ""
  echo "Options:"
  echo "  --path <directory>    Create solution in specified directory (default: current)"
  echo "  --update              Add projects to existing solution without overwriting"
  echo "  --git-init            Initialize git repository with .gitignore"
  echo "  --git-commit          Create initial commit (implies --git-init)"
  echo "  --list                List current solution structure"
  echo "  --rename-legacy       Rename existing projects with .Legacy suffix"
  echo "  --migration-guide     Generate migration guide markdown"
  echo "  --dry-run             Show what would happen without making changes"
  echo ""
  echo "Examples:"
  echo "  $0 RocketChat                              # Create in current directory"
  echo "  $0 RocketChat --path ~/Documents/Projects  # Create in specific location"
  echo "  $0 RocketChat --update                     # Add projects to existing solution"
  echo "  $0 RocketChat --git-init                   # Create with git setup"
  echo "  $0 RocketChat --list                       # Show current structure"
  echo "  $0 RocketChat --rename-legacy --dry-run    # Preview legacy renaming"
  echo "  $0 RocketChat --migration-guide            # Generate refactoring guide"
  exit 0
fi

# Check for solution name
if [ -z "$1" ]; then
  echo -e "${RED}Error: No solution name provided.${NC}"
  echo "Use --help for usage instructions."
  exit 1
fi

SOLUTION_NAME="$1"
UPDATE_MODE=false
GIT_INIT=false
GIT_COMMIT=false
LIST_MODE=false
RENAME_LEGACY=false
MIGRATION_GUIDE=false
DRY_RUN=false
TARGET_PATH=""

# Parse flags
shift
while [ $# -gt 0 ]; do
  case "$1" in
    --path)
      if [ -z "$2" ] || [[ "$2" == --* ]]; then
        print_error "Error: --path requires a directory argument"
        exit 1
      fi
      TARGET_PATH="$2"
      shift
      ;;
    --update)
      UPDATE_MODE=true
      ;;
    --git-init)
      GIT_INIT=true
      ;;
    --git-commit)
      GIT_INIT=true
      GIT_COMMIT=true
      ;;
    --list)
      LIST_MODE=true
      ;;
    --rename-legacy)
      RENAME_LEGACY=true
      UPDATE_MODE=true
      ;;
    --migration-guide)
      MIGRATION_GUIDE=true
      ;;
    --dry-run)
      DRY_RUN=true
      ;;
    *)
      echo -e "${RED}Unknown option: $1${NC}"
      echo "Use --help for usage instructions."
      exit 1
      ;;
  esac
  shift
done

# Function to print colored messages
print_success() {
  echo -e "${GREEN}✓${NC} $1"
}

print_warning() {
  echo -e "${YELLOW}⚠️${NC}  $1"
}

print_error() {
  echo -e "${RED}❌${NC} $1"
}

print_info() {
  echo -e "${BLUE}ℹ${NC}  $1"
}

# Function to execute or preview command
execute_or_preview() {
  if [ "$DRY_RUN" = true ]; then
    echo -e "${BLUE}[DRY RUN]${NC} $1"
  else
    eval "$1"
  fi
}

# Function to check if project exists
project_exists() {
  [ -d "$1" ] && [ -f "$1/$1.csproj" ]
}

# Function to check if project is in solution
project_in_solution() {
  if [ -f "$SOLUTION_NAME.sln" ]; then
    grep -q "$1/$1.csproj" "$SOLUTION_NAME.sln"
    return $?
  fi
  return 1
}

# Function to list solution structure
list_solution_structure() {
  if [ ! -f "$SOLUTION_NAME.sln" ]; then
    print_error "Solution '$SOLUTION_NAME.sln' not found!"
    exit 1
  fi
  
  echo "=========================================="
  echo "Solution Structure: $SOLUTION_NAME"
  echo "=========================================="
  echo ""
  
  echo "Projects in solution:"
  dotnet sln "$SOLUTION_NAME.sln" list | tail -n +3 | while read -r project; do
    echo "  • $project"
  done
  echo ""
  
  echo "Project references:"
  for proj in Domain DAL BL UI; do
    if [ -f "$proj/$proj.csproj" ]; then
      echo ""
      echo "  $proj:"
      refs=$(dotnet list "$proj/$proj.csproj" reference 2>/dev/null | tail -n +3)
      if [ -z "$refs" ]; then
        echo "    (no references)"
      else
        echo "$refs" | while read -r ref; do
          echo "    → $(basename "$ref" .csproj)"
        done
      fi
    fi
  done
  echo ""
  
  if [ -d .git ]; then
    echo "Git status:"
    git status --short
  fi
}

# Function to rename existing projects to legacy
rename_to_legacy() {
  if [ ! -f "$SOLUTION_NAME.sln" ]; then
    print_error "Solution '$SOLUTION_NAME.sln' not found!"
    exit 1
  fi
  
  echo "=========================================="
  echo "Renaming Projects to Legacy"
  echo "=========================================="
  echo ""
  
  # Get list of existing projects (excluding the new ones we want to create)
  local projects_to_rename=()
  
  if [ -d Domain ] && [ ! -f Domain/.newproject ]; then
    projects_to_rename+=("Domain")
  fi
  if [ -d DAL ] && [ ! -f DAL/.newproject ]; then
    projects_to_rename+=("DAL")
  fi
  if [ -d BL ] && [ ! -f BL/.newproject ]; then
    projects_to_rename+=("BL")
  fi
  if [ -d UI ] && [ ! -f UI/.newproject ]; then
    projects_to_rename+=("UI")
  fi
  
  if [ ${#projects_to_rename[@]} -eq 0 ]; then
    print_info "No projects to rename"
    return
  fi
  
  for proj in "${projects_to_rename[@]}"; do
    local new_name="${proj}.Legacy"
    
    if [ -d "$new_name" ]; then
      print_warning "Project '$new_name' already exists, skipping rename of '$proj'"
      continue
    fi
    
    print_info "Renaming '$proj' to '$new_name'..."
    
    if [ "$DRY_RUN" = false ]; then
      # Remove from solution
      dotnet sln "$SOLUTION_NAME.sln" remove "$proj/$proj.csproj" 2>/dev/null || true
      
      # Rename directory
      mv "$proj" "$new_name"
      
      # Rename .csproj file
      mv "$new_name/$proj.csproj" "$new_name/$new_name.csproj"
      
      # Update RootNamespace in .csproj
      sed -i '' "s/<RootNamespace>$proj<\/RootNamespace>/<RootNamespace>$new_name<\/RootNamespace>/g" "$new_name/$new_name.csproj"
      
      # Add back to solution
      dotnet sln "$SOLUTION_NAME.sln" add "$new_name/$new_name.csproj"
      
      print_success "Renamed '$proj' to '$new_name'"
    else
      echo -e "${BLUE}[DRY RUN]${NC} Would rename '$proj' to '$new_name'"
    fi
  done
  echo ""
}

# Function to create .gitignore
create_gitignore() {
  if [ "$DRY_RUN" = true ]; then
    print_info "[DRY RUN] Would create .gitignore"
    return
  fi
  
  cat > .gitignore << 'EOF'
# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Ww][Ii][Nn]32/
[Aa][Rr][Mm]/
[Aa][Rr][Mm]64/
bld/
[Bb]in/
[Oo]bj/
[Ll]og/
[Ll]ogs/

# Visual Studio cache/options
.vs/
.vscode/
*.suo
*.user
*.userosscache
*.sln.docstates

# Rider
.idea/
*.sln.iml

# User-specific files
*.rsuser
*.suo
*.user
*.userosscache
*.sln.docstates

# NuGet Packages
*.nupkg
*.snupkg
**/packages/*
!**/packages/build/

# Test Results
[Tt]est[Rr]esult*/
[Bb]uild[Ll]og.*
*.trx

# .NET Core
project.lock.json
project.fragment.lock.json
artifacts/

# Mac
.DS_Store

# Windows
Thumbs.db
ehthumbs.db
Desktop.ini
EOF
  
  print_success "Created .gitignore"
}

# Function to initialize git repository
init_git() {
  if [ -d .git ]; then
    print_warning "Git repository already exists"
    return
  fi
  
  if [ "$DRY_RUN" = true ]; then
    print_info "[DRY RUN] Would initialize git repository"
    return
  fi
  
  git init
  print_success "Initialized git repository"
  
  create_gitignore
  
  if [ "$GIT_COMMIT" = true ]; then
    git add .
    git commit -m "Initial commit: $SOLUTION_NAME solution structure

- Added Domain project (core entities)
- Added DAL project (data access layer)
- Added BL project (business logic)
- Added UI project (user interface)
- Configured project dependencies"
    print_success "Created initial commit"
  fi
}

# Function to generate migration guide
generate_migration_guide() {
  local guide_file="MIGRATION_GUIDE.md"
  
  if [ "$DRY_RUN" = true ]; then
    print_info "[DRY RUN] Would create $guide_file"
    return
  fi
  
  cat > "$guide_file" << EOF
# Migration Guide: $SOLUTION_NAME Refactoring

Generated: $(date)

## Overview

This guide helps you migrate your existing codebase to the new solution structure with Domain, DAL, BL, and UI layers.

## New Structure

\`\`\`
$SOLUTION_NAME/
├── Domain/          # Core business entities and interfaces
├── DAL/            # Data access layer (repositories, DbContext)
├── BL/             # Business logic (services, validators)
└── UI/             # User interface layer
\`\`\`

## Migration Steps

### 1. Identify Current Code

Review your existing projects and identify:
- **Entities/Models** → Move to \`Domain/\`
- **Database code** → Move to \`DAL/\`
- **Business rules** → Move to \`BL/\`
- **UI/Controllers** → Move to \`UI/\`

### 2. Move Entities to Domain

\`\`\`bash
# Example: Moving entity classes
# From: OldProject/Models/User.cs
# To:   Domain/Entities/User.cs
\`\`\`

**Domain should contain:**
- Entity classes
- Interfaces (IRepository, IService)
- Enums
- Value objects
- Domain exceptions

### 3. Move Data Access to DAL

**DAL should contain:**
- DbContext classes
- Repository implementations
- Data models (if different from entities)
- Migrations
- Database configurations

### 4. Move Business Logic to BL

**BL should contain:**
- Service classes
- Validation logic
- Business rules
- DTOs (Data Transfer Objects)
- Mappers

### 5. Update UI Layer

**UI should contain:**
- Program.cs / Startup
- Controllers (if web app)
- Views
- API endpoints
- Dependency injection configuration

## Dependency Guidelines

- ✅ DAL can reference Domain
- ✅ BL can reference Domain and DAL
- ✅ UI can reference BL and Domain
- ❌ Domain should not reference any other project
- ❌ DAL should not reference BL or UI
- ❌ BL should not reference UI

## Checklist

- [ ] Identify all entities and move to Domain
- [ ] Move database code to DAL
- [ ] Extract business logic to BL
- [ ] Update UI to use BL services
- [ ] Update namespaces throughout
- [ ] Fix all project references
- [ ] Update dependency injection
- [ ] Run tests
- [ ] Update documentation

## Common Pitfalls

1. **Circular dependencies** - Ensure dependencies only flow downward
2. **Mixing concerns** - Keep business logic out of DAL
3. **Direct DB access from UI** - Always go through BL
4. **Forgetting interfaces** - Define interfaces in Domain

## Testing Strategy

1. Test Domain entities in isolation
2. Mock DAL in BL tests
3. Integration tests for DAL
4. End-to-end tests in UI

## Rollback Plan

If issues arise:
1. Git branch allows easy rollback
2. Legacy projects remain available (with .Legacy suffix)
3. Incremental migration is possible

## Next Steps

1. Start with Domain layer (no dependencies)
2. Then DAL (depends only on Domain)
3. Then BL (depends on Domain and DAL)
4. Finally UI (depends on BL)

## Resources

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [.NET Project Structure](https://docs.microsoft.com/en-us/dotnet/architecture/)

---
**Note:** Take your time with migration. Test frequently and commit working states.
EOF
  
  print_success "Created $guide_file"
}

# Function to safely create project
safe_create_project() {
  local project_type=$1
  local project_name=$2
  local project_path=$3
  
  if project_exists "$project_name"; then
    print_warning "Project '$project_name' already exists, skipping creation..."
    return 1
  else
    print_success "Creating project '$project_name'..."
    execute_or_preview "dotnet new $project_type -n '$project_name' -o '$project_path' --framework net9.0"
    
    # Remove default Class1.cs files
    if [ "$DRY_RUN" = false ] && [ "$project_type" = "classlib" ] && [ -f "$project_path/Class1.cs" ]; then
      rm "$project_path/Class1.cs"
      print_info "  Removed default Class1.cs"
    fi
    
    # Mark as new project for legacy detection
    if [ "$DRY_RUN" = false ]; then
      touch "$project_path/.newproject"
    fi
    
    return 0
  fi
}

# Function to safely add project to solution
safe_add_to_solution() {
  local project_name=$1
  
  if project_in_solution "$project_name"; then
    print_warning "Project '$project_name' already in solution, skipping..."
  else
    print_success "Adding '$project_name' to solution..."
    execute_or_preview "dotnet sln '$SOLUTION_NAME.sln' add '$project_name/$project_name.csproj'"
  fi
}

# Function to safely add reference
safe_add_reference() {
  local from_project=$1
  local to_project=$2
  
  if [ "$DRY_RUN" = false ] && dotnet list "$from_project/$from_project.csproj" reference 2>/dev/null | grep -q "$to_project.csproj"; then
    print_warning "Reference from '$from_project' to '$to_project' already exists, skipping..."
  else
    print_success "Adding reference from '$from_project' to '$to_project'..."
    execute_or_preview "dotnet add '$from_project/$from_project.csproj' reference '$to_project/$to_project.csproj'"
  fi
}

# Main execution

# Handle --list mode
if [ "$LIST_MODE" = true ]; then
  list_solution_structure
  exit 0
fi

# Handle --migration-guide mode
if [ "$MIGRATION_GUIDE" = true ]; then
  generate_migration_guide
  exit 0
fi

# Handle path parameter - change to target directory
ORIGINAL_DIR=$(pwd)
if [ -n "$TARGET_PATH" ]; then
  # Expand tilde and make absolute path
  TARGET_PATH="${TARGET_PATH/#\~/$HOME}"
  
  if [ "$DRY_RUN" = true ]; then
    print_info "[DRY RUN] Would create/navigate to directory: $TARGET_PATH"
  else
    # Create directory if it doesn't exist
    if [ ! -d "$TARGET_PATH" ]; then
      mkdir -p "$TARGET_PATH"
      print_success "Created directory: $TARGET_PATH"
    fi
    
    # Change to target directory
    cd "$TARGET_PATH" || {
      print_error "Failed to change to directory: $TARGET_PATH"
      exit 1
    }
    print_info "Working in: $(pwd)"
  fi
  echo ""
fi

if [ "$DRY_RUN" = true ]; then
  echo -e "${BLUE}=========================================="
  echo "DRY RUN MODE - No changes will be made"
  echo "==========================================${NC}"
  echo ""
fi

echo "=========================================="
echo "RocketDotnet Setup: $SOLUTION_NAME"
echo "=========================================="
echo ""

# Check if we're in a git repository
if [ -d .git ]; then
  print_info "Git repository detected - all operations are safe for version control"
  echo ""
fi

# Handle legacy renaming
if [ "$RENAME_LEGACY" = true ]; then
  rename_to_legacy
fi

# Create or check solution
if [ -f "$SOLUTION_NAME.sln" ]; then
  if [ "$UPDATE_MODE" = true ]; then
    print_success "Using existing solution '$SOLUTION_NAME.sln'"
    echo ""
  else
    print_error "Solution '$SOLUTION_NAME.sln' already exists!"
    echo "   Use --update flag to add projects to existing solution:"
    echo "   $0 $SOLUTION_NAME --update"
    exit 1
  fi
else
  if [ "$UPDATE_MODE" = true ]; then
    print_error "Solution '$SOLUTION_NAME.sln' not found!"
    echo "   Remove --update flag to create a new solution."
    exit 1
  fi
  print_success "Creating solution '$SOLUTION_NAME'..."
  execute_or_preview "dotnet new sln -n '$SOLUTION_NAME'"
  echo ""
fi

# Create projects
echo "Creating projects..."
echo "--------------------"
safe_create_project classlib Domain Domain
safe_create_project classlib DAL DAL
safe_create_project classlib BL BL
safe_create_project console UI UI
echo ""

# Add projects to solution
echo "Adding projects to solution..."
echo "------------------------------"
safe_add_to_solution Domain
safe_add_to_solution DAL
safe_add_to_solution BL
safe_add_to_solution UI
echo ""

# Add references
echo "Setting up project references..."
echo "--------------------------------"
safe_add_reference DAL Domain
safe_add_reference BL Domain
safe_add_reference BL DAL
safe_add_reference UI BL
safe_add_reference UI Domain
echo ""

# Git setup
if [ "$GIT_INIT" = true ]; then
  echo "Git setup..."
  echo "------------"
  init_git
  echo ""
fi

# Clean up marker files
if [ "$DRY_RUN" = false ]; then
  rm -f Domain/.newproject DAL/.newproject BL/.newproject UI/.newproject 2>/dev/null || true
fi

# Show final structure
echo "=========================================="
print_success "Setup complete!"
echo "=========================================="
echo ""
echo "Solution structure:"
echo "  $SOLUTION_NAME/"
echo "    ├── Domain/          (class library)"
echo "    ├── DAL/             (data access → Domain)"
echo "    ├── BL/              (business logic → Domain, DAL)"
echo "    └── UI/              (console app → BL, Domain)"
echo ""

if [ -d .git ]; then
  echo "Git status:"
  echo "  - New files are untracked and safe"
  echo "  - Existing files remain unchanged"
  echo "  - Run 'git status' to review changes"
  echo ""
fi

if [ "$DRY_RUN" = true ]; then
  echo -e "${BLUE}This was a dry run. No changes were made.${NC}"
  echo "Remove --dry-run flag to execute these changes."
  echo ""
fi

echo "Useful commands:"
echo "  $0 $SOLUTION_NAME --list              # Show solution structure"
echo "  $0 $SOLUTION_NAME --migration-guide   # Generate refactoring guide"
if [ -n "$TARGET_PATH" ]; then
  echo "  cd $TARGET_PATH                       # Navigate to solution"
fi
echo "  dotnet build                          # Build solution"
echo "  dotnet run --project UI               # Run UI project"
echo ""

# Return to original directory if we changed it
if [ -n "$TARGET_PATH" ] && [ "$DRY_RUN" = false ]; then
  cd "$ORIGINAL_DIR"
fi