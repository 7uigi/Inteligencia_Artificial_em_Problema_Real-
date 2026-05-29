using Unity.InferenceEngine;
// using Unity.Sentis;
// using Unity.Barracuda
// using Unity.mlagents
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public ModelAsset modelAsset;

    private Worker worker;

    void Start()
    {
        Model runtimeModel = ModelLoader.Load(modelAsset);

        worker = new Worker(runtimeModel, BackendType.CPU);
    }

    void Update()
    {
        float[] obs = GetObservations();

        using Tensor<float> inputTensor =
            new Tensor<float>(new TensorShape(1, obs.Length), obs);

        worker.Schedule(inputTensor);

        Tensor<float> output = worker.PeekOutput() as Tensor<float>;

        int action = GetBestAction(output);

        ExecuteAction(action);

        output.Dispose();
    }

    float[] GetObservations()
    {
        return new float[]
        {
            transform.position.x,
            transform.position.y
        };
    }

    int GetBestAction(Tensor<float> output)
    {
        int bestAction = 0;
        float bestValue = output[0];

        for (int i = 1; i < output.shape.length; i++)
        {
            if (output[i] > bestValue)
            {
                bestValue = output[i];
                bestAction = i;
            }
        }

        return bestAction;
    }

    void ExecuteAction(int action)
    {
        switch (action)
        {
            case 0:
                transform.Translate(Vector3.left * Time.deltaTime);
                break;

            case 1:
                transform.Translate(Vector3.right * Time.deltaTime);
                break;

            case 2:
                transform.Translate(Vector3.up * Time.deltaTime);
                break;

            case 3:
                transform.Translate(Vector3.down * Time.deltaTime);
                break;
        }
    }

    void OnDestroy()
    {
        worker?.Dispose();
    }
}
