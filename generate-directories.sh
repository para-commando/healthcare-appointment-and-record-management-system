#!/bin/bash

# Array of directory/project names
project_names=(
  "appointments-management"
  "authentication-management"
  "clinical-data-grid"
  "inventory-management"
  "patient-management"
  "prescription-management"
  "staff-management"
)

# Loop through each name and create the project if it doesn't exist
for project_name in "${project_names[@]}"; do
  if [ -d "$project_name" ]; then
    echo "Skipping: $project_name (already exists)"
  else
    echo "Creating project: $project_name"
    dotnet new web --name "$project_name" || {
      echo "Failed to create project: $project_name"
      exit 1
    }
  fi
done

echo "Script execution complete!"
