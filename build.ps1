$REPO = "192.168.31.21:5000/lira-sharp/playground"

podman build `
  --tag $REPO `
  --cache-from $REPO `
  --cache-from $REPO/cache `
  --cache-to   $REPO/cache `
  .

podman push $REPO
