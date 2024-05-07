Configuration
git config --global user.name "[name]": Set a name that will be attached to your commits.
git config --global user.email "[email address]": Set an email address that will be attached to your commits.
git config --global color.ui auto: Enable helpful colorization of command line output.
Creating Repositories
git init [project-name]: Create a new local repository.
git clone [url]: Clone a repository into a new directory.
Making Changes
git status: List all new or modified files to be committed.
git add [file]: Add a file to the staging area.
git add .: Add all new and modified files to the staging area.
git commit -m "[descriptive message]": Commit changes to head (but not yet to the remote repository).
git commit -am "[descriptive message]": Add and commit changes in one command.
Branching & Merging
git branch: List all local branches in the current repository.
git branch [branch-name]: Create a new branch.
git checkout [branch-name]: Switch to a different branch.
git merge [branch]: Merge changes from another branch into your current branch.
git branch -d [branch-name]: Delete a branch.
Remote Repositories
git remote add [alias] [url]: Add a new remote repository.
git push [alias] [branch]: Upload local repository content to a remote repository.
git pull [alias] [branch]: Fetch changes from a remote repository and merge them into the current branch.
git remote -v: List all remote repositories.
Undoing Changes
git reset [file]: Unstage a file while retaining its changes.
git reset --hard: Discard all local changes in your working directory.
git checkout -- [file]: Discard changes in the working directory since the last commit.
Inspection & Comparison
git log: Show the commit history.
git log --oneline: Show the commit history with abbreviated commit hashes.
git diff: Show changes between commits, commit and working tree, etc.
git show [commit]: Show changes made in a commit.
Tags
git tag [tag-name]: Create a new tag for the current commit.
git tag -a [tag-name] -m "[message]": Create an annotated tag.
git push [alias] --tags: Push tags to a remote repository.
git checkout [tag-name]: Switch to a specific tag.
