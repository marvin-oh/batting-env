## Installation

```
pip install mlagents==0.29.0
```

### Run Training
```
mlagents-learn config/vanilla.yaml --env=Build/three-sided-football.exe
```
```
usage: mlagents-learn [-h] [--env ENV_PATH] [--resume] [--force] [--run-id RUN_ID] [--initialize-from RUN_ID]
                          [--seed SEED] [--inference] [--base-port BASE_PORT] [--num-envs NUM_ENVS] [--debug]
                          [--env-args ...] [--torch] [--tensorflow] [--results-dir RESULTS_DIR] [--width WIDTH]
                          [--height HEIGHT] [--quality-level QUALITY_LEVEL] [--time-scale TIME_SCALE]
                          [--target-frame-rate TARGET_FRAME_RATE] [--capture-frame-rate CAPTURE_FRAME_RATE]
                          [--no-graphics] [--torch-device DEVICE]
                          [trainer_config_path]
```

### Tensorboard
```
tensorboard --logdir=results --port=6006
```
