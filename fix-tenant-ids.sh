#!/bin/bash

# Fix TenantId compilation errors in test files
# This script replaces int TenantId values with appropriate Guid values

# Define the test directories
TEST_DIRS=(
    "tests/maERP.Server.Tests"
    "tests/maERP.Persistence.Tests"
    "tests/maERP.Web.Tests"
)

echo "Fixing TenantId compilation errors in test files..."

# Add TenantConstants import where needed
for dir in "${TEST_DIRS[@]}"; do
    if [ -d "$dir" ]; then
        find "$dir" -name "*.cs" -type f | while read file; do
            # Check if file uses TenantId = number patterns and doesn't have TenantConstants import
            if grep -q "TenantId.*=.*[0-9]" "$file" && ! grep -q "using maERP.Domain.Constants;" "$file"; then
                # Add the import after other maERP imports
                if grep -q "using maERP" "$file"; then
                    sed -i '' '/using maERP.Domain.Entities;/a\
using maERP.Domain.Constants;
' "$file" 2>/dev/null || true
                fi
            fi
        done
    fi
done

# Fix specific TenantId patterns
for dir in "${TEST_DIRS[@]}"; do
    if [ -d "$dir" ]; then
        find "$dir" -name "*.cs" -type f | while read file; do
            echo "Processing $file..."
            
            # Replace TenantId = 1 with TenantId = TenantConstants.TestTenant1Id
            sed -i '' 's/TenantId = 1[^0-9]/TenantId = TenantConstants.TestTenant1Id/g' "$file"
            
            # Replace TenantId = 2 with TenantId = TenantConstants.TestTenant2Id
            sed -i '' 's/TenantId = 2[^0-9]/TenantId = TenantConstants.TestTenant2Id/g' "$file"
            
            # Replace TenantId == 1 with TenantId == TenantConstants.TestTenant1Id
            sed -i '' 's/TenantId == 1[^0-9]/TenantId == TenantConstants.TestTenant1Id/g' "$file"
            
            # Replace TenantId == 2 with TenantId == TenantConstants.TestTenant2Id
            sed -i '' 's/TenantId == 2[^0-9]/TenantId == TenantConstants.TestTenant2Id/g' "$file"
            
            # Replace new List<int> with new List<Guid> for tenant collections
            sed -i '' 's/new List<int>/new List<Guid>/g' "$file"
            
            # Replace HashSet<int> with HashSet<Guid> for tenant collections
            sed -i '' 's/HashSet<int>/HashSet<Guid>/g' "$file"
            
            # Replace IReadOnlyCollection<int> with IReadOnlyCollection<Guid>
            sed -i '' 's/IReadOnlyCollection<int>/IReadOnlyCollection<Guid>/g' "$file"
            
            # Replace IEnumerable<int> with IEnumerable<Guid> for tenant methods
            sed -i '' 's/IEnumerable<int> tenantIds/IEnumerable<Guid> tenantIds/g' "$file"
            
            # Replace int tenantId parameters with Guid tenantId
            sed -i '' 's/int tenantId)/Guid tenantId)/g' "$file"
            sed -i '' 's/int? tenantId)/Guid? tenantId)/g' "$file"
            
            # Replace specific test values
            sed -i '' 's/TenantId = 999/TenantId = Guid.NewGuid()/g' "$file"
            sed -i '' 's/tenantId.ToString()/tenantId.ToString()/g' "$file"
        done
    fi
done

echo "Finished processing TenantId fixes."
echo "Note: You may need to manually review and fix any remaining compilation errors."