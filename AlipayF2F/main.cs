using System.Net;

namespace AlipayF2F;

using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Domain;

public class F2FClient
{
    private IAopClient _client;

    public F2FClient(string appId,
        string priKeyPem,
        string appCertPath,
        string rootCertPath,
        string alipayPublicCertPath
    )
    {
        _client = new DefaultAopClient(
            "https://openapi.alipaydev.com/gateway.do",
            appId,
            priKeyPem,
            "json", "1.0", "RSA2",
            "utf-8",
            false,
            new CertParams
            {
                AppCertPath = appCertPath,
                RootCertPath = rootCertPath,
                AlipayPublicCertPath = alipayPublicCertPath
            }
        );
    }

    public T ExecuteRequest<T>(IAopRequest<T> request) where T : AopResponse
    {
        return _client.CertificateExecute(request);
    }
}

public class F2FRequest
{
    //trade status:

    public static AlipayTradePrecreateRequest PreCreateTrade
    (string outTradeNo, string tradeMsg,
        double amount)
    {
        var request = new AlipayTradePrecreateRequest();

        var model = new AlipayTradePrecreateModel
        {
            Subject = tradeMsg,
            OutTradeNo = outTradeNo,
            TotalAmount = amount.ToString("f2")
        };

        request.SetBizModel(model);

        return request;
    }

    public static AlipayTradeCreateRequest CreateTrade
    (string outTradeNo, string tradeMsg,
        long sellerId,
        long buyerId,
        double amount)
    {
        var request = new AlipayTradeCreateRequest();

        var model = new AlipayTradeCreateModel
        {
            SellerId = sellerId.ToString(),
            BuyerId = buyerId.ToString(),
            TotalAmount = amount.ToString("f2"),
            Subject = tradeMsg,
            OutTradeNo = outTradeNo
        };

        request.SetBizModel(model);

        return request;
    }

    public static AlipayTradeQueryRequest QueryTrade
        (string outTradeNo)
    {
        var request = new AlipayTradeQueryRequest();

        var model = new AlipayTradeQueryModel
        {
            OutTradeNo = outTradeNo
        };

        request.SetBizModel(model);

        return request;
    }

    public static AlipayTradeCancelRequest CancelTrade
        (string outTradeNo)
    {
        var request = new AlipayTradeCancelRequest();

        var model = new AlipayTradeCancelModel
        {
            OutTradeNo = outTradeNo
        };

        request.SetBizModel(model);

        return request;
    }

    public static AlipayTradeRefundRequest RefundTrade
        (string outTradeNo, double amount)
    {
        var request = new AlipayTradeRefundRequest();

        var model = new AlipayTradeRefundModel
        {
            OutTradeNo = outTradeNo,
            RefundAmount = amount.ToString("f2")
        };

        request.SetBizModel(model);

        return request;
    }

    public static AlipayTradeCloseRequest CloseTrade
        (string outTradeNo)
    {
        var request = new AlipayTradeCloseRequest();

        var model = new AlipayTradeCloseModel()
        {
            OutTradeNo = outTradeNo,
        };

        request.SetBizModel(model);

        return request;
    }
}

public class F2FResponse
{
    /// <summary>
    /// TRADE_SUCCESS（交易支付成功）
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static bool IsTradeSuccess(AlipayTradeQueryResponse response)
    {
        return response.TradeStatus == "TRADE_SUCCESS";
    }

    /// <summary>
    /// TRADE_FINISHED（交易结束，不可退款）
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static bool IsTradeFinished(AlipayTradeQueryResponse response)
    {
        return response.TradeStatus == "TRADE_FINISHED";
    }

    /// <summary>
    /// WAIT_BUYER_PAY（交易创建，等待买家付款）
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static bool IsTradeWaitingPay(AlipayTradeQueryResponse response)
    {
        return response.TradeStatus == "WAIT_BUYER_PAY";
    }

    /// <summary>
    /// TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）
    /// TODO 此API场景未知
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static bool IsTradeClosed(AlipayTradeQueryResponse response)
    {
        return response.TradeStatus == "TRADE_CLOSED";
    }
}