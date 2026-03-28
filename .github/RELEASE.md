# Release Checklist

When preparing a release, follow these steps:

## Pre-Release

- [ ] Update version number in `Application.myapp` 
- [ ] Update `CHANGELOG.md` with release notes
- [ ] Update `docs/release-notes.md`
- [ ] Run full test suite
- [ ] Build Release configuration successfully
- [ ] Review all changes since last release

## Create Release

1. Ensure all changes are committed and pushed
2. Create a git tag:
   ```bash
   git tag -a v1.x.x -m "Release version 1.x.x"
   git push origin v1.x.x
   ```

3. GitHub Actions will automatically:
   - Build the application
   - Create a release draft

## Post-Release

- [ ] Review GitHub Release automatically created
- [ ] Add release notes and artifacts
- [ ] Publish the release
- [ ] Announce on relevant channels

---

**Note:** This project uses semantic versioning (MAJOR.MINOR.PATCH).
