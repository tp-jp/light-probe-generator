# LightProbeGenerator

LigthProbe を簡単に配置できる便利ツールです。同時に配置禁止領域も指定できます。  
VRChatのワールド作成時などにご利用ください。

## 導入方法

VCCをインストール済みの場合、以下の**どちらか一つ**の手順を行うことでインポートできます。

- [https://tp-jp.github.io/vpm-repos/](https://tp-jp.github.io/vpm-repos/) へアクセスし、「Add to VCC」をクリック

- VCCのウィンドウで `Setting - Packages - Add Repository` の順に開き、 `https://tp-jp.github.io/vpm-repos/index.json` を追加

[VPM CLI](https://vcc.docs.vrchat.com/vpm/cli/) を使用してインストールする場合、コマンドラインを開き以下のコマンドを入力してください。

```
vpm add repo https://tp-jp.github.io/vpm-repos/index.json
```
VCCから任意のプロジェクトを選択し、「Manage Project」から「Manage Packages」を開きます。
一覧の中から `LightProbeGenerator` の右にある「＋」ボタンをクリックするか「Installed Vection」から任意のバージョンを選択することで、プロジェクトにインポートします。
![image](https://github.com/tp-jp/LightProbeGenerator/assets/130125691/790fc4f0-3dd9-43f8-9a2d-317379869bdd)

## 使い方

1. Packages/LightProbeGenerator/Runtime/Prefab/LightProbeGenerator.prefab を Hierarchy にドラッグ＆ドロップします。
![image](https://github.com/tp-jp/LightProbeGenerator/assets/130125691/f0775d17-3d69-4ac5-9c35-48fd9bd1bf0c)

2. Hierarchy上の LightProbeGenerator を選択し、Inspector を表示します。
![image](https://github.com/tp-jp/LightProbeGenerator/assets/130125691/bb8002e6-3887-4542-9dbf-4f722aba7804)

3. Inspector上で設定を行います。   
   - Transform     
     LightProbeGeneratorのギズモを表示することで、LightProbeを配置する領域が確認できます。 Transformを変更し、配置する領域を指定します。
   
   - ProbeSpacing     
     LightProbeを配置したい間隔を指定します。
   
   - LightProbeの配置を禁止する領域     
     Addボタンを押下することで、禁止領域（ProhibitedArea）が生成されます。 LightProbe を配置したくない領域がある場合は適宜指定します。

4. Inspector上で LightProbeGenerator のGenerateボタンを押下することで LightProbe が生成されます。
![image](https://github.com/tp-jp/LightProbeGenerator/assets/130125691/a48c2e37-2485-4dea-a7ab-40c6cd20e229)
![image](https://github.com/tp-jp/LightProbeGenerator/assets/130125691/3bb59b87-93d5-4961-b439-93fc72fcf533)

