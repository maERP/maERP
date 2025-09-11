#!/bin/bash

# Fix syntax errors caused by the sed script
echo "Fixing syntax errors in test files..."

# Fix broken parentheses patterns
find tests -name "*.cs" -type f | while read file; do
    echo "Processing $file..."
    
    # Fix .Where(p => p.TenantId == TenantConstants.TestTenant1Id -> .Where(p => p.TenantId == TenantConstants.TestTenant1Id)
    sed -i '' 's/TenantId == TenantConstants.TestTenant1Id$/TenantId == TenantConstants.TestTenant1Id)/' "$file"
    sed -i '' 's/TenantId == TenantConstants.TestTenant2Id$/TenantId == TenantConstants.TestTenant2Id)/' "$file"
    
    # Fix queries with missing closing parentheses
    sed -i '' 's/\.Where(i => i\.TenantId == TenantConstants\.TestTenant1Id$/\.Where(i => i.TenantId == TenantConstants.TestTenant1Id)/' "$file"
    sed -i '' 's/\.Where(i => i\.TenantId == TenantConstants\.TestTenant2Id$/\.Where(i => i.TenantId == TenantConstants.TestTenant2Id)/' "$file"
    
    # Fix other common patterns
    sed -i '' 's/\.Where(p => p\.TenantId == TenantConstants\.TestTenant1Id$/\.Where(p => p.TenantId == TenantConstants.TestTenant1Id)/' "$file"
    sed -i '' 's/\.Where(p => p\.TenantId == TenantConstants\.TestTenant2Id$/\.Where(p => p.TenantId == TenantConstants.TestTenant2Id)/' "$file"
    
    # Fix .CountAsync(p => p.TenantId == TenantConstants patterns
    sed -i '' 's/\.CountAsync(p => p\.TenantId == TenantConstants\.TestTenant1Id$/\.CountAsync(p => p.TenantId == TenantConstants.TestTenant1Id)/' "$file"
    sed -i '' 's/\.CountAsync(p => p\.TenantId == TenantConstants\.TestTenant2Id$/\.CountAsync(p => p.TenantId == TenantConstants.TestTenant2Id)/' "$file"
    
    # Fix any remaining patterns
    sed -i '' 's/ == TenantConstants\.TestTenant1Id$/ == TenantConstants.TestTenant1Id)/' "$file"
    sed -i '' 's/ == TenantConstants\.TestTenant2Id$/ == TenantConstants.TestTenant2Id)/' "$file"
done

echo "Finished fixing syntax errors."