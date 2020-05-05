# TK Limit
Controls how people can TK in SCP:SL using EXILED

| Option          | Value  | Default                     | Explanation                                                      |
|-----------------|--------|-----------------------------|------------------------------------------------------------------|
| tkl_enable      | bool   | true                        | Is the plugin enabled?                                           |
| tkl_log         | bool   | true                        | Does the plugin print when a play tks to the console?            |
| tkl_limiter     | bool   | true                        | Does the plugin limit tk to a certain amount of times per round? |
| tkl_ban         | bool   | false                       | Does the plugin ban you if you get to the tk limit?              |
| tkl_bantime     | int    | 0                           | How long does the plugin ban you for? (0 for kick)               |
| tkl_warning     | bool   | false                       | Does the plugin warn you about your last tk?                     |
| tkl_warningbc   | string | "You have one tk left"      | What is the warning message?                                     |
| tkl_warningtime | int    | 5                           | How long does the message last for?                              |
| tkl_room        | bool   | false                       | Does the plugin tell you if your in a room you can't tk in?      |
| tkl_roombc      | string | "You can't tk in this room" | What is the room message?                                        |
| tkl_roomtime    | int    | 5                           | How long does the message last for?                              |
| tkl_max         | bool   | false                       | Does the plugin tell you if you've reached the tk limit?         |
| tkl_maxbc       | string | "You can't tk anymore"      | What is the max message?                                         |
| tkl_maxtime     | int    | 5                           | How long does the message last for?                              |
| tkl_limit       | int    | 4                           | How many times can a player tk per round?                        |
| tkl_rooms       | list   | { }                         | What rooms does the plugin disable tk in?                        |
| tkl_zones       | list   | { }                         | What zones does the plugin disable tk in?                        |