﻿namespace MyTested.Mvc.Builders.Attributes
{
    using Microsoft.AspNet.Mvc;
    using Contracts.Attributes;
    using System;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Authorization;

    /// <summary>
    /// Used for testing controller attributes.
    /// </summary>
    public class ControllerAttributesTestBuilder : BaseAttributesTestBuilder, IAndControllerAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerAttributesTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller which will be tested.</param>
        public ControllerAttributesTestBuilder(Controller controller)
            : base(controller)
        {
        }

        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ChangingRouteTo(
                template,
                this.ThrowNewAttributeAssertionException,
                withName,
                withOrder);

            return this;
        }

        // TODO: RoutePrefix was moved to single Route attribute
        ///// <summary>
        ///// Checks whether the collected attributes contain RoutePrefixAttribute.
        ///// </summary>
        ///// <param name="prefix">Expected overridden route prefix of the controller.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndControllerAttributesTestBuilder ChangingRoutePrefixTo(string prefix)
        //{
        //    this.ContainingAttributeOfType<RoutePrefixAttribute>();
        //    this.Validations.Add(attrs =>
        //    {
        //        var routePrefixAttribute = this.GetAttributeOfType<RoutePrefixAttribute>(attrs);
        //        var actualRoutePrefix = routePrefixAttribute.Prefix;
        //        if (prefix != actualRoutePrefix)
        //        {
        //            this.ThrowNewAttributeAssertionException(
        //                string.Format("{0} with '{1}' prefix", routePrefixAttribute.GetName(), prefix),
        //                string.Format("in fact found '{0}'", actualRoutePrefix));
        //        }
        //    });

        //    return this;
        //}

        /// <summary>
        /// Checks whether the collected attributes contain AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <summary>
        /// Checks whether the collected attributes contain AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null)
        {
            this.RestrictingForAuthorizedRequests(
                this.ThrowNewAttributeAssertionException,
                withAllowedRoles);

            // TODO: authorize attribute has policy and schemes

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining attribute tests.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IControllerAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to have {1}, but {2}.",
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
