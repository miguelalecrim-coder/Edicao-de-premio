using MassTransit;


public class EdicaoState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public string Descricao { get; set; } = default!;

    public string CurrentState { get; set; } = default!;
}