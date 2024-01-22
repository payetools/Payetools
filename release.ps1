param($tag)
if ($PSBoundParameters.Keys.Count -ne 1) {
    Write-Error 'Usage: release <version_number>'
} else {
    git tag -a $tag -m "Release $tag"
    git push --follow-tags
}